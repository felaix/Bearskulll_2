using System;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class WaveController : MonoBehaviour
{

    public static WaveController Instance { get; private set; }

    private int currentWave = 1;
    private int killedEnemyCount;
    private bool isWaveCompleted;
    private bool isWaveOnGoing;

    private SpawnController spawnController;

    public Action StartWave;
    public Action EndWave = () => Debug.Log("Wave conpleted");
    public void KilledEnemy()
    {
        killedEnemyCount++;
        //Debug.Log("Killed enemy count: " + killedEnemyCount);
    }

    private void Update()
    {
        if (isWaveOnGoing)
        {
            if (killedEnemyCount >= currentWave) WaveCompleted();
        }
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void WaveCompleted()
    {
        isWaveCompleted = true;
        isWaveOnGoing = false;
        killedEnemyCount = 0;
        EndWave();
    }

    private void Start()
    {
        spawnController = GetComponent<SpawnController>();

        if (spawnController == null) 
        {
            spawnController = CreateSpawnController(); 
        }
    }

    private SpawnController CreateSpawnController()
    {
        SpawnController spawnController = gameObject.AddComponent<SpawnController>();
        return spawnController;
    }

    public void TriggerWave()
    {
        if (isWaveOnGoing) 
        {
            return;
        }

        if (currentWave == 0)
        {
            isWaveOnGoing = true;
            currentWave++;
            StartWave();
        }

        if (!isWaveCompleted) return;
        isWaveCompleted = false;
        isWaveOnGoing = true;  
        currentWave++;
        StartWave();
    }

    public int GetCurrentWaveCount() => currentWave;

    public int GetCurrentKilledEnemyCount() => killedEnemyCount;

    public bool IsWaveOnGoing() => isWaveOnGoing;

}
