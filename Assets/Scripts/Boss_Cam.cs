using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Cam : MonoBehaviour
{

    public bool PlayerIsInArena;
    public Animator CMAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            PlayerIsInArena = true;
            CMAnimator.SetBool("boss", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            PlayerIsInArena = false;
            CMAnimator.SetBool("boss", false);
        }
    }

}
