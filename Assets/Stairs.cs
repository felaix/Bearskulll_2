using UnityEngine;

public class Stairs : MonoBehaviour
{

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

        if (!spawned) { _animator.Play("BlockUP"); Debug.Log("UP STAIRS"); }
        else { _animator.Play("BlockDOWN"); Debug.Log("DOWN STAIRS"); }
    }
}
