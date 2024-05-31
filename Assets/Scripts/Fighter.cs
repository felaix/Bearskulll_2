using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fighter : MonoBehaviour, IAction
{

    [SerializeField] Health _target;

    public Transform RightHandTransform = null;
    public Transform LeftHandTransform = null;

    public  Weapon _weapon = null;
    public Weapon _shield;
    
    private float lastHit;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _AttackFX;
    [SerializeField] GameObject _AttackFX2;
    [SerializeField] int AttackID;

    private float _atkSpeedBoost = 1;
    private bool powerActiv = false;
    [SerializeField] GameObject PowerUpFx;

    [SerializeField] AudioClip EquipSFX, AttackSFX1, AttackSFX2;


    public bool useSkill;
    [SerializeField] GameObject _SkillactivFX;
    private bool knockedOut;
    [SerializeField] GameObject _knockOutFX;

    bool haveweaponnotfist = false;
    [SerializeField] Weapon fist;

    private bool isBlocking;

    private void Start()
    {
        EquipWeapon(_weapon);
        if (_shield != null) _shield.EquipShield(RightHandTransform, LeftHandTransform);
    }

    private void Update()
    {
        if (knockedOut && !GetComponent<Health>().isDead)
        {
            Cancel();
            if (_knockOutFX != null) _knockOutFX.SetActive(true);
            return;
        }
        else
            if (_knockOutFX != null) _knockOutFX.SetActive(false);

        if (_target != null && _target.isDead)
            Cancel();

        if (_target == null) return;

        if (_target != null && !GetInRange())
        {
            transform.LookAt(_target.transform.position);
            GetComponent<Movement>().MoveTo(_target.transform.position, 1);
        }
        else
        {
            GetComponent<Movement>().Cancel();
            AttackBehavior();
        }

        if (fist != null)
        {
            if (_weapon != fist)
            { haveweaponnotfist = true; }
            else { haveweaponnotfist = false; }
        }
    }


    public void EquipWeapon(Weapon _weaponEquip)
    {
        if(_weaponEquip == null) return;
        Debug.Log("Equip weapon: " +  _weaponEquip);
        _weapon = _weaponEquip;
        _weaponEquip.Spawn(RightHandTransform , LeftHandTransform);
        AudioManager.instance.PlayEffect(EquipSFX);
    }


    public void TriggerBlock(bool block) => isBlocking = block;

    public bool IsBlocking() => isBlocking;

    void AttackBehavior()
    {
        if (Time.time - lastHit > _weapon.weaponSpeed * _atkSpeedBoost)
        {
            lastHit = Time.time;
        //    if(this.gameObject.GetComponent<Health>().isDead == false)
            StartCoroutine(AttackHit());
        }
    }

    public IEnumerator AttackHit() 
    {
        transform.LookAt(_target.transform.position);

        yield return new WaitForSeconds(0.3f);

        if (_target != null)
        {         
            if (!_weapon.isRangeWeapon)
            {
                if (useSkill)
                {
                    AudioManager.instance.PlayEffect(AttackSFX2);
                    yield return new WaitForSeconds(0.2f);
                    _anim.SetTrigger("Skill");
                    if (_target != null)
                    {
                        Instantiate(_AttackFX2, _target.transform.position, Quaternion.identity);
                        _target.TakeDamage(_weapon.weaponDamage+5, haveweaponnotfist);
                        StartCoroutine(_target.GetComponent<Fighter>().KnockOut());
                    }
                    useSkill = false;
                }
                else
                {
                    AttackID = Random.Range(0, 3);
                    _anim.SetTrigger("Attack");
                    AudioManager.instance.PlayEffect(AttackSFX1);
                    _anim.SetInteger("AttackID", AttackID);
                    yield return new WaitForSeconds(0.2f);
                    if (_target != null)
                    {
                        Instantiate(_AttackFX, _target.transform.position, Quaternion.identity);
                        _target.TakeDamage(_weapon.weaponDamage, haveweaponnotfist);
                    }
                }
            }
            else
            {
                if (_weapon.leftHanded)
                { 
                    _anim.SetBool("Shoot", true);
                    yield return new WaitForSeconds(0.15f); 
                }
                else
                {       
                    AttackID = Random.Range(1, 3);
                    _anim.SetTrigger("Attack");
                    yield return new WaitForSeconds(0.2f);
                }
                
                _weapon.LaunchProjectile(RightHandTransform, LeftHandTransform, _target);
                AudioManager.instance.PlayEffect(AttackSFX2);
                yield return new WaitForSeconds(0.15f);
                _anim.SetBool("Shoot", false);
            }
        }

    }

    public IEnumerator  KnockOut()
    {
        if (GetComponent<Fighter>().knockedOut != true)
        {
            GetComponent<Fighter>().knockedOut = true;
            yield return new WaitForSeconds(3);
            GetComponent<Fighter>().knockedOut = false;
        }

    }

    public void SkillButton()
    {
        if(!useSkill)
            StartCoroutine(SkillUse());
    }
    public IEnumerator SkillUse()
    {
        useSkill = true;
        _SkillactivFX.SetActive(true);
        yield return new WaitForSeconds(3);
        useSkill = false;
        _SkillactivFX.SetActive(false);
    }

    private bool GetInRange()
    {
        return Vector3.Distance(transform.position, _target.transform.position) < _weapon.weaponRange;
    }

    public void Attack(GameObject attacktarget)
    {
        GetComponent<ActionState>().StartAction(this);
        _target = attacktarget.GetComponent<Health>();
    }

    public void TakeEnergy()
    {
        if (!powerActiv)
            StartCoroutine(TakeEnergyCO());
        else
            GetComponent<Inventory>().addEnergy();
    }

    public IEnumerator TakeEnergyCO()
    {
        powerActiv = true;
        PowerUpFx.SetActive(powerActiv);
        float originSpeed = GetComponent<Movement>().maxSpeed;

        GetComponent<Movement>().maxSpeed = GetComponent<Movement>().maxSpeed * 1.5f;
        _atkSpeedBoost = 0.5f;

        yield return new WaitForSeconds(5);

        _atkSpeedBoost = 1f;
        GetComponent<Movement>().maxSpeed = originSpeed;
        powerActiv = false;
        PowerUpFx.SetActive(powerActiv);
    }

    public void Cancel()
    {
       // _anim.SetTrigger("StopAttack");
        _target = null;
        lastHit -= 1;
        GetComponent<Movement>().Cancel();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_weapon != null) Gizmos.DrawWireSphere(transform.position, _weapon.weaponRange);
    }
}
