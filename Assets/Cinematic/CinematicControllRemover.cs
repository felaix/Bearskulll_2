using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class CinematicControllRemover : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _disableControlsOnLoad = false;
    [SerializeField] private GameObject _ui;

    private void Awake()
    {
        Debug.Log(GetComponent<PlayableDirector>());
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;

    }

    private void Start()
    {
        if (_disableControlsOnLoad) DisableControl(GetComponent<PlayableDirector>());
    }

    private void DisableControl(PlayableDirector pd)
    {
        _player = GameObject.FindWithTag("Player");
        _player.GetComponent<ActionState>().CancelCurrentAction();
        _player.GetComponent<Player>().enabled = false;
        _player.GetComponent<Movement>().ToggleSkipUpdate(true);

        _ui.SetActive(false);

        Debug.Log("Disable control (cinematic controll remover)");
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
        await Task.Delay(100);

        _ui.SetActive(true);

        _player.GetComponent<Player>().enabled = true;
        _player.GetComponent<Movement>().ToggleSkipUpdate(false);

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

