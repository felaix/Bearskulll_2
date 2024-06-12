using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCutscene : MonoBehaviour
{

    [SerializeField] private List<Transform> _playerDestinations;

    [SerializeField] private GameObject _knight;
    private GameObject _player;
    private Movement _playerMovement;
    private Fighter _playerFighter;


    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerMovement = _player.transform.GetComponent<Movement>();
        _playerFighter = _player.transform.GetComponent<Fighter>();

        Invoke("Cutscene", 1.5f);
    }

    private void Cutscene()
    {
        _playerMovement.StartMoveAction(_playerDestinations[0].position, 1);
        _playerFighter.Attack(_knight);
    }
}
