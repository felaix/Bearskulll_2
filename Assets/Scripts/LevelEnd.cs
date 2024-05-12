using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{

    [SerializeField] GameObject _objectToMove;
    [SerializeField] bool _isPressed;
    
    [SerializeField] AudioClip _LevelEndSFX;
    [SerializeField] GameObject _keyMissingText;

    private void Update()
    {
        _objectToMove.GetComponent<Animator>().SetBool("Pressed", _isPressed);
       
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Inventory>().key != 0)
            {
                AudioManager.instance.PlayEffect(_LevelEndSFX);
                other.GetComponent<Inventory>().key = 0;
                _isPressed = true;
            }
            if(other.GetComponent<Inventory>().key == 0 && !_isPressed)
            {
                if (_keyMissingText.activeSelf != true)
                      StartCoroutine(KeyMissingText());
            }
        }
    }

    IEnumerator KeyMissingText()
    {
        _keyMissingText.SetActive(true);
        yield return new WaitForSeconds(2);
        _keyMissingText.SetActive(false);
    }

}
