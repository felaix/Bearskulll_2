using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;




    public class CinematicControllRemover : MonoBehaviour
    {
        [SerializeField]private GameObject _player;
 

    private void Awake()
        {
            Debug.Log(GetComponent<PlayableDirector>());
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

    private void DisableControl(PlayableDirector pd)
        {
            _player = GameObject.FindWithTag("Player");
            _player.GetComponent<ActionState>().CancelCurrentAction();
            _player.GetComponent<Player>().enabled = false;
        }
    private void EnableControl(PlayableDirector pd)
        {
            _player.GetComponent<Player>().enabled = true;
        }
    }

