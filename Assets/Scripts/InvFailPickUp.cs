using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvFailPickUp : MonoBehaviour
{

    public Image HealthImage;
    public Image EnergyImage;
    bool stop1 = false;
    bool stop2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HealthImage.color == Color.red && !stop1)
        {
            StartCoroutine(HealthFailPickUpTimer());
            stop1 = true;
        }

        if (EnergyImage.color == Color.red && !stop2)
        {
            StartCoroutine(EnergyFailPickUpTimer());
            stop2 = true;
        }
    }

    public void HealthFailPickUp()
    {
        HealthImage.color = Color.red;
        
    }
    public void EnergyFailPickUp()
    {
        EnergyImage.color = Color.red;
        
    }
    

    IEnumerator HealthFailPickUpTimer()
    {
        yield return new WaitForSeconds(1f);
        HealthImage.color = new Color32(178, 172, 172, 255);
        stop1 = false;
    }

    IEnumerator EnergyFailPickUpTimer()
    {
        yield return new WaitForSeconds(1f);
        EnergyImage.color = new Color32(178, 172, 172, 255);
        stop2 = false;
    }

}
