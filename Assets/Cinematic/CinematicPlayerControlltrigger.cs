using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPlayerControlltrigger : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _hud;
    

    public void takeControlls()
    {
        _player.enabled = false;
        _hud.SetActive(false);
    }

    public void giveControlls()
    {
        _player.enabled = true;
        _hud.SetActive(true);
    }

}
