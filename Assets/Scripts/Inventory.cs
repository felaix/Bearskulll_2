using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   
    public int heart, energy, key;
    public int heartmax, energymax;
    public Text hearttxt, energytxt, keytxt;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            HealButton();
        if(Input.GetKeyDown(KeyCode.Alpha2))
            EnergyButton();

        hearttxt.text = heart.ToString();
        energytxt.text = energy.ToString();
        keytxt.text = key.ToString();
    }

    private void Start()
    {
        energymax = 2;
        heartmax = 4;

        if (SaveGame.Exists("BigBag"))
        {
            energymax = 3;
            heartmax = 5;
        }
        if (SaveGame.Exists("SuperBag"))
        {
            energymax = 4;
            heartmax = 7;
        }
    }



    public void HealButton()
    {
        if (heart > 0)
        {
            heart--;
            GetComponent<Health>().TakeHealing(75);
        }
        else
            Debug.Log("NoHeartLeft!");
    }
    public void EnergyButton()
    {
        if (energy > 0)
        {
            energy--;
            GetComponent<Fighter>().TakeEnergy();
        }
        else
            Debug.Log("NoEnergyLeft!");
    }
    
    public void addKey()
    {
      
        if(key < 1)
            key++;
    }
    public void addHeal()
    {
        if(heart < heartmax)
            heart++;
    }
    public void addEnergy()
    {
        if(energy < energymax)
        energy++;
    }

}
