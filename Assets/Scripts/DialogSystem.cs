using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{

    [SerializeField] private string[] speachlines;
    [SerializeField] private Sprite faceImage;
    [SerializeField] private bool _destroyAfterTrigger;
    [SerializeField] private float textSpeed;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!DialogBoxText.instance.textStarted)
            {
                DialogBoxText.instance.SetDialog(speachlines, faceImage, textSpeed);
                DialogBoxText.instance.TriggerDialog();
                if (_destroyAfterTrigger)
                    Destroy(gameObject,0.1f);
            }
          
        }
    }

    public void DialogCall()
    {
        if (!DialogBoxText.instance.textStarted)
        {
            DialogBoxText.instance.SetDialog(speachlines, faceImage, textSpeed);
            DialogBoxText.instance.TriggerDialog();
            if (_destroyAfterTrigger)
                Destroy(gameObject, 0.1f);
        }
    }


}


