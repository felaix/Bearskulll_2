using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Transform playerT;
    public float spawnRadius = 5.0f;
    public GameObject enemyPrefab;
    public GameObject enemyPortalPrefab;

    private int spawnAmount = 1;

    private List<GameObject> _enemies = new List<GameObject>();

    private Coroutine spawnLoop;
    private Transform _currentPortal;

    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        WaveController.Instance.StartWave += TriggerWave;
        WaveController.Instance.StopWave += StopWave;
    }

    private void StopWave()
    {
        StopCoroutine(spawnLoop);
        //_enemies.ForEach(e => { e.GetComponent<Health>().TakeDamage(9999, false); });
        //_enemies.Clear();
    }
    private async void TriggerWave()
    {
        if (WaveController.Instance != null) spawnAmount = WaveController.Instance.GetCurrentWaveCount();
        //Debug.Log("Spawn amount:" + spawnAmount);

        await Task.Delay(1000);

        _currentPortal = SpawnPortal();

        spawnLoop = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(1f);
        _enemies.Clear();

        int x = 0;

        while (x < spawnAmount)
        {
            x++;
            GameObject enemy = SpawnEnemy();
            _enemies.Add(enemy);
            yield return new WaitForSeconds(1f);
        }
    }

    private Transform SpawnPortal()
    {
        Vector3 correctedSpawnPoint = GetSpawnPointNearPlayer() + new Vector3(0, 2, 0);
        return Instantiate(enemyPortalPrefab, correctedSpawnPoint, Quaternion.identity).transform;
    }

    private GameObject SpawnEnemy()
    {
        //Debug.Log("Spawn Enemy");
        return Instantiate(enemyPrefab, _currentPortal.position, Quaternion.identity);
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
