using System;
using System.ComponentModel;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class WaveController : MonoBehaviour
{

    public static WaveController Instance { get; private set; }

    private int currentWave = 1;
    private int killedEnemyCount = 0;

    private bool isWaveCompleted = false;
    private bool isWaveOnGoing = false;

    private SpawnController spawnController;

    // Start & End
    public Action StartWave;
    public Action EndWave;

    public void KilledEnemy()
    {
        Debug.Log("Killed enemy");
        killedEnemyCount++;

        //Debug.Log("Killed enemy count: " + killedEnemyCount);
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void WaveCompleted()
    {
        //Debug.Log("Killed enemys: " + killedEnemyCount);

        // Wave ist completed und wave ist nicht mehr on going
        isWaveCompleted = true;
        isWaveOnGoing = false;

        // Reset enemy count für die nächste wave
        killedEnemyCount = 0;

        // Level up
        currentWave++;

        // trigger end wave um mögl. listener zu triggern
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
        if (isWaveOnGoing || !isWaveCompleted) return;

        isWaveCompleted = false;
        isWaveOnGoing = true;

        StartWave();
    }

    public int GetCurrentWaveCount() => currentWave;

    public int GetCurrentKilledEnemyCount() => killedEnemyCount;

    public bool IsWaveOnGoing() => isWaveOnGoing;

}
