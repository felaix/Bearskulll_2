using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaModusBasic : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject[] SpawnPoints;
    public GameObject[] SpawnPointsTrank;

    public float SpawnTime = 7.5f;

    public int Spawned;

    public GameObject[] EnemiesEasy;
    public GameObject[] EnemiesNormal;
    public GameObject[] EnemiesHard;
    public GameObject[] Tranks;




    void Start()
    {
        InvokeRepeating("SpawnEnemies", 2.0f, SpawnTime);
        InvokeRepeating("SpawnTranke", 0, 12.9f);
    }



    public void SpawnEnemies()
    {

        // Random Spawn Point Selection, with Length of SpawnPoints Array
        int SpawnPoint = Random.Range(0, SpawnPoints.Length);

        if (Spawned <= 8)
        {
            // Random Enemy Selection, with Length of Enemies Array

            int Enemy = Random.Range(0, EnemiesEasy.Length);

            // Spawn Enemy at SpawnPoint Position
            Instantiate(EnemiesEasy[Enemy], SpawnPoints[SpawnPoint].transform.position, SpawnPoints[SpawnPoint].transform.rotation);
        }

        else if (Spawned <= 23)
        {

            int Enemy = Random.Range(0, EnemiesNormal.Length);

            // Spawn Enemy at SpawnPoint Position
            Instantiate(EnemiesNormal[Enemy], SpawnPoints[SpawnPoint].transform.position, SpawnPoints[SpawnPoint].transform.rotation);



        }

        else if (Spawned >= 20)
        {

            int Enemy = Random.Range(0, EnemiesHard.Length);

            // Spawn Enemy at SpawnPoint Position
            Instantiate(EnemiesHard[Enemy], SpawnPoints[SpawnPoint].transform.position, SpawnPoints[SpawnPoint].transform.rotation);
        }

        if (Spawned % 10 == 0)
        {
            SpawnTime -= 0.30f;
        }


        Spawned++;



    }



    public void SpawnTranke()
    {

        // Random Spawn Point Selection, with Length of SpawnPoints Array
        int SpawnPoint = Random.Range(0, SpawnPointsTrank.Length);

            // Random Enemy Selection, with Length of Enemies Array

            int Trankss = Random.Range(0, Tranks.Length);

            // Spawn Enemy at SpawnPoint Position
            Instantiate(Tranks[Trankss], SpawnPointsTrank[SpawnPoint].transform.position, SpawnPointsTrank[SpawnPoint].transform.rotation);
        




    }
}
