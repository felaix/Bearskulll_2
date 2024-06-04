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
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

} 
