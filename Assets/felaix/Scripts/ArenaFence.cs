using DG.Tweening;
using Unity.Collections;
using UnityEngine;

public class ArenaFence : MonoBehaviour
{
    [SerializeField] GameObject _keyMissingCanvas;
    [ReadOnly][SerializeField] private bool _triggered = false;
    [SerializeField] private AudioClip _clipOpenGate;


    private void OnTriggerEnter(Collider other)
    {

        if (_triggered) return;

        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Inventory>().key != 0)
            {
                _keyMissingCanvas.SetActive(false);
                transform.DOMoveY(-10f, 5f);
                AudioManager.instance.PlayEffect(_clipOpenGate);
                _triggered = true;
            }else
            {
                _keyMissingCanvas.SetActive(true);
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        _keyMissingCanvas?.SetActive(false);
    }
}
