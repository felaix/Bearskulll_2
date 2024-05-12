using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject miniBossDoorGood;
    public GameObject miniBossDoorBad;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       
            miniBossDoorGood.SetActive(true);
            miniBossDoorBad.SetActive(false);
            Destroy(this.gameObject);
    }

}
