using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;




   
    public class CinematicTrigger : MonoBehaviour
    {
        [HideInInspector] public bool _triggerSet = true;
        [SerializeField] bool _playOnAwake;

    private void Start()
    {
        if(_playOnAwake)
        {
            GetComponent<PlayableDirector>().Play();
            _triggerSet = false;
        }
    }
    private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _triggerSet)
            {
                GetComponent<PlayableDirector>().Play();
                _triggerSet = false;
            }
        }
    }

