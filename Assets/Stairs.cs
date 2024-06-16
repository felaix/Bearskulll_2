using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stairs : MonoBehaviour
{
    [SerializeField] private List<NavMeshObstacle> _obstacles;
    private Animator _animator;
    private bool spawned;

    private void Awake()
    {
    }

    private void Start()
    {
        WaveController.Instance.StartWave += SpawnStairs;
        WaveController.Instance.EndWave += SpawnStairs;

        _animator = GetComponent<Animator>();
    }

    private void SpawnStairs()
    {
        spawned = !spawned;

        if (!spawned) { _animator.Play("UP"); Debug.Log("UP STAIRS"); DeactivateObstacles(); }
        else { _animator.Play("DOWN"); Debug.Log("DOWN STAIRS"); ActivateObstacles(); }
    }

    private void DeactivateObstacles()
    {
        foreach(var obstacle in _obstacles)
        {
            obstacle.enabled = false;
        }
    }

    private void ActivateObstacles()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.enabled = true;
        }
    }

}
