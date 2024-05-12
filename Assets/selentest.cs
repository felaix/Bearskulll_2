using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class selentest : MonoBehaviour
{
    public TMP_Text text;
    public int selenint;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("loadselen", 0, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "soulS: " + selenint;
    }

    public void loadselen() {


        selenint = SaveGame.Load<int>("Souls");


    }



}
