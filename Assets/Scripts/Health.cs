using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
using System;

public class Health : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette vignette;
    public bool isDead;
    [SerializeField] bool isPlayer;
    [SerializeField] public int _HP = 100;
    public int _curHP;
    [SerializeField] Image _hpBarImage;
    [SerializeField] CanvasGroup _cgDmg;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform _FXtransform;
    [SerializeField] GameObject healFX;
    [SerializeField] CinemachineImpulseSource impulse;
    public bool isArenaChest = false;
    public bool IsChest = false;
    public bool IsChestHuge = false;
    public bool isInvincible = false;
    public bool isShield = false;

    private Color blockUIColor = new(255, 167, 0);
    private Color defaultUIColor = new(0, 255, 226);
    private Fighter fighter;
    private void Start()
    {
        _curHP = _HP;
        if (volume != null)
        {
            volume.profile.TryGetSettings(out vignette);

        }

        fighter = GetComponent<Fighter>();
    }

    public void ActivateInvincibility() { isInvincible = true; SetHPBarCustomColor(Color.yellow); }

    private void SetHPBarCustomColor(Color color)
    {
        _hpBarImage.color = color;
    }

    private void SetHPBarColor() => _ = fighter.IsBlocking() ? _hpBarImage.color = blockUIColor : _hpBarImage.color = defaultUIColor;

    public void DeactivateInvincibility() { isInvincible = false; SetHPBarCustomColor(Color.red); }

    private void Update()
    {

        if (fighter.IsBlocking()) isInvincible = true;
        else isInvincible = false;

        if (_hpBarImage)
            UIUpdate();

        if (isPlayer && volume != null)
        {
            if (_curHP <= 30)
            {
                //  vignette.enabled.value = true;
                //   vignette.color.value = Color.red;
                // Pulsing effect with minimum intensity
                //    float pulsingSpeed = 2f;
                //   float minimumIntensity = 0.67f;
                //     float pulsingRange = 0.33f;
                //  vignette.intensity.value = minimumIntensity + pulsingRange * Mathf.Abs(Mathf.Sin(Time.time * pulsingSpeed));
            }
            else
            {
                // Normal black vignette, no pulsing
                //    vignette.color.value = Color.black;
                //     vignette.intensity.value = 0.67f; // adjust this as per your needs
            }
        }

    }

    public void TakeDamage(int damage, bool weapon)
    {
        if (isInvincible) return;

        if (isArenaChest)
        {
            _curHP -= damage;
        }

        if (IsChest && weapon)
        {
            _curHP -= damage;
        }

        if (!IsChest)
        {
            _curHP -= damage;
        }

        if (_curHP <= 0)
        {
            if (isArenaChest)
            {
                GetComponent<Enemy>().ItemDrop();
                StartCoroutine(DeathFX());
                //Destroy(gameObject, .5f);
            }

            if (isShield)
            {
                BossController.instance.OnShieldDestroyed();
            }

            if (IsChestHuge)
            {
                Archievments.Instance.UnlockAchievement("BOX_MYSTERY");
            }

            _curHP = 0;
            _anim.SetBool("dead", true);

            GetComponent<Collider>().enabled = false;
            if (!isPlayer && !isDead)
            {
                StartCoroutine(DeathFX());
            }
            else
                isDead = true;
        }
        if (isPlayer)
            impulse.GenerateImpulse();

    }
    public void TakeHealing(int healamount)
    {

        _curHP += healamount;
        if (_curHP >= _HP)
        {
            _curHP = _HP;
        }
        Instantiate(healFX, transform.position, Quaternion.identity);
    }

    void UIUpdate()
    {
        if (isPlayer) SetHPBarColor();
        _hpBarImage.fillAmount = (float)_curHP / (float)_HP;
        if (isPlayer)
            _cgDmg.alpha = 0.7f - (float)_curHP / (float)_HP;
    }

    IEnumerator DeathFX()

    {
        isDead = true;
        if (Archievments.Instance != null) Archievments.Instance.IncrementStat();
        GetComponent<NavMeshAgent>().enabled = false;
        try
        {
            GetComponent<Fighter>().enabled = false;
        }
        catch { }


        float speed = 2f;
        if (IsChest || isArenaChest)
        {
            speed = 0.2f;
        }

        yield return new WaitForSeconds(speed);
        Destroy(gameObject, speed);
        yield return new WaitForSeconds(speed - 0.1f);
        if (deathFX != null && _FXtransform != null) Instantiate(deathFX, _FXtransform.position, Quaternion.identity);
    }

}
