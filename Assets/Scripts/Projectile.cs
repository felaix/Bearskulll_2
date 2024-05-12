using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] Health _targetHealth = null;
    [SerializeField] float _speed = 1;
    [SerializeField] int _damage;
    [SerializeField] bool _homming;
    [SerializeField] Collider _collider;
    [SerializeField] GameObject _AttackFX;
    

    private void Start()
    {
       if(!_homming)
       if (_targetHealth != null)
                transform.LookAt(GetAimLocation());
        Destroy(gameObject, 8f);
    }
    void Update()
    {
        if (_targetHealth == null) return;

        if (_targetHealth.isDead)
            Destroy(gameObject);

        if (_homming)
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

    }

    public void SetTarget(Health target, int projectiledamage)
    {
        this._targetHealth = target;
        this._damage = projectiledamage;
    }
    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = _targetHealth.GetComponent<CapsuleCollider>();
        if (targetCapsule== null)
        {
            return _targetHealth.transform.position;
        }
        return _targetHealth.transform.position + (Vector3.up * (targetCapsule.height * 0.6f));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Obstacle>() != null)
        {
            Instantiate(_AttackFX, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.01f);
        }
        if (other.GetComponent<Health>() == _targetHealth) 
        { 
            _targetHealth.TakeDamage(_damage, true);
            Instantiate(_AttackFX, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.01f);
        }


    }

}
