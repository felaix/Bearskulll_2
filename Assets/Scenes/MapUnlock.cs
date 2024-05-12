using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapUnlock : MonoBehaviour
{
    public string Levelname;
    public GameObject Bridge;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveGame.Exists(Levelname))
        {
            Bridge.GetComponent<NavMeshObstacle>().enabled = false;
            this.gameObject.SetActive(false);

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
