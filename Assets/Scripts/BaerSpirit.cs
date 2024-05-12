using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaerSpirit : MonoBehaviour
{
    [SerializeField] GameObject _playerFollowTarget;
    [SerializeField] AudioClip _foundSomething;
    [SerializeField] float _searchDistance = 10;
    
    [SerializeField] Vector3 _offset;
    private int Priority;
    private Transform _target;
    [SerializeField] Rotator _rotator;
    float _playerDist;
    void Start()
    {
        
    }

   
    void Update()
    {
        _playerDist = Vector3.Distance(transform.position, _playerFollowTarget.transform.position);


        if (!Inportant())
        {

            transform.position = Vector3.Lerp(transform.position, _playerFollowTarget.transform.position, 0.03f);
            transform.rotation = Quaternion.Lerp(transform.rotation, _playerFollowTarget.transform.rotation, 0.1f);
        }
        else
        {
            if (Vector3.Distance(transform.position, _target.position) > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, _target.position + _offset, 0.025f);
                transform.LookAt(_playerFollowTarget.transform);
            }
        }

        _rotator.enabled = Inportant();
    }

    private bool Inportant()
    {
        if (_playerDist > _searchDistance)
        {
            Priority = -1;
            return false;
            
        }
        if (_target != null && Vector3.Distance(_playerFollowTarget.transform.position, _target.transform.position) < _searchDistance)
        {
            return true;
        }
        else
        {
            _target = null;
            Priority = -1;
            return false;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Interactable>() != null)
        {
            if (Priority < other.GetComponent<Interactable>().Priority)
            {
                Priority = other.GetComponent<Interactable>().Priority;
                _target = other.GetComponent<Transform>();
                AudioManager.instance.PlayEffect(_foundSomething);
            }
        }
    }

}
