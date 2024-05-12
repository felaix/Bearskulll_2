using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using UnityEngine.AI;

public class MapLoader : MonoBehaviour
{
    public GameObject Player;

    public static MapLoader Instance;

    public NavMeshAgent PlayerAgent;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        if (SaveGame.Exists("PlayerPosition"))
        {
            Player.transform.position = SaveGame.Load<Vector3>("PlayerPosition");
            PlayerAgent.Warp(SaveGame.Load<Vector3>("PlayerPosition"));
          //  Player.transform.position = SaveGame.Load<Vector3>("PlayerPosition");
          Invoke("LoadPlayer", 0.1f);
        }
        else
        {
            PlayerAgent.Warp(new Vector3(-12.694f, 0.799f, 10.344f));
        }
    }

    public void LoadPlayer()
    {
            PlayerAgent.Warp(SaveGame.Load<Vector3>("PlayerPosition"));

    }
    public void SavePlayer()
    {

        SaveGame.Save<Vector3>("PlayerPosition", Player.transform.position);

    }


}
