using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stairs : MonoBehaviour
{
    [SerializeField] private List<NavMeshObstacle> _obstacles;
    private Animator _animator;
    //private bool spawned;

    private void Awake()
    {
    }

    private void Start()
    {
        WaveController.Instance.StartWave += () => SpawnStairs(false);
        WaveController.Instance.EndWave += () => SpawnStairs(true);

        _animator = GetComponent<Animator>();
    }

    private void SpawnStairs(bool spawn)
    {
        //spawned = !spawned;

        if (spawn) { _animator.Play("UP"); DeactivateObstacles(); }
        else { _animator.Play("DOWN"); ActivateObstacles(); }
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
