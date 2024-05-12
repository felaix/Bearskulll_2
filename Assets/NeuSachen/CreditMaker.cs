using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditMaker : MonoBehaviour
{
    public GameObject[] CreditObjects;
    public GameObject MainCredit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<CanvasGroup>().alpha = MainCredit.GetComponent<CanvasGroup>().alpha;
    }
}
