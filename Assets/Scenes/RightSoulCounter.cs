using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightSoulCounter : MonoBehaviour
{

    public int souls = 0;
    public TMP_Text soulText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        souls = SaveManager.instance.Souls;
        soulText.text = souls.ToString();
    }
}
