using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using BayatGames.SaveGameFree;

public class SaveSystem : MonoBehaviour
{

    public Inventory InventoryPlayer;

    public static SaveSystem instance;

    public string Levelname;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        LoadEverything();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEverything()
    {
        try 
        {
            InventoryPlayer.energy = SaveGame.Load<int>("Energyy.txt");
            InventoryPlayer.heart = SaveGame.Load<int>("Healpots.txt");
        }

        catch { }
        
    }


    public void endsave()
    {



        SaveGame.Save<int>("Energyy.txt", InventoryPlayer.energy);
        SaveGame.Save<int>("Healpots.txt", InventoryPlayer.heart);

        SaveGame.Save<bool>(Levelname, true);


    }




}
