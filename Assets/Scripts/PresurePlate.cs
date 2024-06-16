using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PresurePlate : MonoBehaviour
{
    [SerializeField] GameObject _objectToMove;
    [SerializeField] bool _hasToBePlayer;
    [SerializeField] bool _hasToStay;
    [SerializeField] bool _isPressed;
    [SerializeField] bool _startWave;
    [SerializeField] private bool _isArena = false;
    public NavMeshObstacle[] navMeshObstacles;

    [SerializeField] AudioClip _pressedSFX;

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

            if (!_isArena) return;

            if (_startWave)
            {
                WaveController.Instance.StartWave();
            }

            _objectToMove.GetComponent<Animator>().Play("DOWN");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            if (_hasToStay)
                _isPressed = false;
        }
    }


}
