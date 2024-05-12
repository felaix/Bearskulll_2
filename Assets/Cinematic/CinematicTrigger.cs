using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CinematicTrigger : MonoBehaviour
{
    [HideInInspector] public bool _triggerSet = true;
    [SerializeField] bool _playOnAwake;

    private PlayableDirector director;


    private void Start()
    {
        director = GetComponent<PlayableDirector>();

        if (_playOnAwake)
        {
            director.Play();
            _triggerSet = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _triggerSet)
        {
            director.Play();
            _triggerSet = false;
        }
    }

    public void SkipCinematic()
    {
        if (!_triggerSet && director != null)
        {
            director.Stop();
            Destroy(director.gameObject, .1f);
        }
    }
}

