using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]GameObject[] DoNotDestroyStuff;


    public void ManagerClear()
    {
        DoNotDestroyStuff[0] = GameObject.Find("DialogSystem");
        DoNotDestroyStuff[1] = GameObject.Find("AudioManager");
        DoNotDestroyStuff[2] = GameObject.Find("SaveManager");

        for (int i = 0; i < DoNotDestroyStuff.Length; i++)
        {
            Destroy(DoNotDestroyStuff[i]);
        }
    }

}
