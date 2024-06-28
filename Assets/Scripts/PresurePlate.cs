using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using DG.Tweening;

public class PresurePlate : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject _objectToMove;
    [SerializeField] GameObject _objectToActivate;

    [Header("Conditions")]
    [SerializeField] bool _hasToBePlayer;
    [SerializeField] bool _hasToStay;
    [SerializeField] bool _isPressed;
    [SerializeField] bool _startWave;

    public NavMeshObstacle[] navMeshObstacles;

    [Header("Arena")]
    [SerializeField] private bool _isArena = false;
    [SerializeField] private bool _useDG = false; // DG stands for DG Tweening. Using DG means NOT to use animator but use DG.Tweening instead

    [Header("Sound & Cinematics")]
    [SerializeField] AudioClip _pressedSFX;
    [SerializeField] private PlayableDirector _directorToPlay;

    private bool _wasPressed;

    private void Update()
    {
        if (_isArena) return;
        _objectToMove.GetComponent<Animator>().SetBool("Pressed", _isPressed);
        GetComponent<Animator>().SetBool("Pressed", _isPressed);
    }

    private void Start()
    {
        foreach (NavMeshObstacle obstacle in navMeshObstacles)
        {
            if (obstacle) obstacle.enabled = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            AudioManager.instance.PlayEffect(_pressedSFX);
           
            _isPressed = true;

            foreach (NavMeshObstacle obstacle in navMeshObstacles)
            {
                if (obstacle) obstacle.enabled = false;
            }

            if (_wasPressed) return;

            if (_objectToActivate != null) _objectToActivate.SetActive(true);

            #region Arena

            // Arena script

            if (!_isArena) return;


            if (_startWave)
            {
                WaveController.Instance.StartWave();
            }

            if (_useDG)
            {
                Debug.Log("Move Obj DG " + _objectToMove);
                _objectToMove.transform.DOMoveY(-5, 2f);
            }
            else { _objectToMove.GetComponent<Animator>().Play("DOWN"); }

            if (_directorToPlay != null)
            {
                _directorToPlay.Play();
            } 
            #endregion

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            _wasPressed = true;

            if (_hasToStay)
                _isPressed = false;
        }
    }


}
