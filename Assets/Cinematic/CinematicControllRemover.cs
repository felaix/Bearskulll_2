using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class CinematicControllRemover : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void Awake()
    {
        Debug.Log(GetComponent<PlayableDirector>());
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;

    }

    private void Start()
    {
        DisableControl(GetComponent<PlayableDirector>());
    }

    private void DisableControl(PlayableDirector pd)
    {
        _player = GameObject.FindWithTag("Player");
        _player.GetComponent<ActionState>().CancelCurrentAction();
        _player.GetComponent<Player>().enabled = false;

        Debug.Log("Disable control");
        // Disable enemy controls too

        if (EnemyCounter.Instance == null) return;

        foreach (Enemy enemy in EnemyCounter.Instance.Enemies)
        {
            Debug.Log("Disable for " + enemy.name);

            enemy.CanAttack = false;
            enemy.enabled = false;
            enemy.GetComponent<ActionState>().CancelCurrentAction(); 
        }

    }
    private async void EnableControl(PlayableDirector pd)
    {
        await Task.Delay(3000);
        _player.GetComponent<Player>().enabled = true;


        Debug.Log("enable control");
        if (EnemyCounter.Instance == null) return;

        // Enable enemy controls
        foreach (Enemy enemy in EnemyCounter.Instance.Enemies)
        {
            enemy.CanAttack = true;

            enemy.enabled = true;

        }
    }
}

