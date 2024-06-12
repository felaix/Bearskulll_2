using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Transform playerT;
    public float spawnRadius = 5.0f;
    public GameObject enemyPrefab;

    private int spawnAmount = 1;

    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        WaveController.Instance.StartWave += TriggerWave;
    }

    private void TriggerWave()
    {
        if (WaveController.Instance != null) spawnAmount = WaveController.Instance.GetCurrentWaveCount();
        Debug.Log("Spawn amount:" + spawnAmount);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        Debug.Log("Spawn enemy coroutine");
        yield return new WaitForSeconds(1f);
        int x = 0;
        List<GameObject> enemies = new List<GameObject>();
        while (x < spawnAmount)
        {
            x++;
            GameObject enemy = SpawnEnemy();
            Debug.Log($"Spawning enemy {enemy}");
            enemies.Add(enemy);
            yield return new WaitForSeconds(1f);
        }

        while (WaveController.Instance.GetCurrentKilledEnemyCount() < spawnAmount)
        {
            yield return null;
        }

        WaveController.Instance.WaveCompleted();
    }

    private GameObject SpawnEnemy()
    {
        Debug.Log("Spawn Enemy");
        return Instantiate(enemyPrefab, GetSpawnPointNearPlayer(), Quaternion.identity);
    }

    private Vector3 GetSpawnPointNearPlayer()
    {

        float angle = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(0, spawnRadius);
        Vector3 spawnPos = new Vector3
            (
            playerT.position.x + distance * Mathf.Cos(angle),
            playerT.position.y,
            playerT.position.z + distance * Mathf.Sin(angle)
            );

        return spawnPos;
    }


}
