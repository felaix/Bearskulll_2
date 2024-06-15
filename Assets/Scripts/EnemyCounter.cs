using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter Instance { get; private set; }
    
    public List<Enemy> Enemies = new();

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Enemies.Clear();
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);

        Debug.Log("Enemy count is " + Enemies.Count);

    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (WaveController.Instance != null) WaveController.Instance.KilledEnemy();
        Enemies.Remove(enemy);

        if (Enemies.Count == 0)
        {
            WaveController.Instance.WaveCompleted();
            Debug.Log("Enemy count is 0");
        }
    }

} 
