using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class WeaponActivation : MonoBehaviour
{

    public Weapon BasicSword;
    public Weapon KnightSword;
    public Weapon HeroSword;
    public Weapon MonsterDagger;
    public Weapon BossAxe;
    public Weapon Shield;
    public Weapon Faueste;

    public Fighter PlayerDeadFighterScript;


    // Start is called before the first frame update
    void Start()
    {
     //   if(SaveGame.Exists("Blacksmith"))
     //   {
            // OVERWRITE ON

            Invoke("OverwriteWeapon", 0.5f);


      //  }
    }




    public void OverwriteWeapon()
    {

        if (SaveGame.Load<string>("Weapon") == "BasicSword")
        {

            PlayerDeadFighterScript.EquipWeapon(BasicSword);

        }

        else if (SaveGame.Load<string>("Weapon") == "KnightSword")
        {

            PlayerDeadFighterScript.EquipWeapon(KnightSword);

        }

        else if (SaveGame.Load<string>("Weapon") == "HeroSword")
        {

            PlayerDeadFighterScript.EquipWeapon(HeroSword);

        }

        else if (SaveGame.Load<string>("Weapon") == "MonsterDagger")
        {

            PlayerDeadFighterScript.EquipWeapon(MonsterDagger);

        }
        else if (SaveGame.Load<string>("Weapon") == "BossAxe")
        {

            PlayerDeadFighterScript.EquipWeapon(BossAxe);

        }

        else
        {

            PlayerDeadFighterScript.EquipWeapon(Faueste);


        }

        if (SaveGame.Load<string>("Weapon") == "Shield")
        {
            PlayerDeadFighterScript.EquipWeapon(Shield);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
