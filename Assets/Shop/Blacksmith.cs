using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using TMPro;
using System;
using UnityEngine.UI;

public class Blacksmith : MonoBehaviour
{

    public Weapon BasicSword;
    public Weapon KnightSword;
    public Weapon HeroSword;
    public Weapon MonsterDagger;
    public Weapon BossAxe;

    public BuyShop buyShop;

    public Sprite[] weaponSprites;
    public Image PreviewImage;
    public TMP_Text PreviewWeaponLvl;
    public TMP_Text PreviewWeaponDamage;
    public TMP_Text PreviewWeaponUpgradeCost;


    public TMP_Text BasicSwordLvl;
    public TMP_Text KnightSwordLvl;
    public TMP_Text HeroSwordLvl;
    public TMP_Text MonsterDaggerLvl;
    public TMP_Text BossAxeLvl;
    public TMP_Text BasicSwordButtonText;
    public TMP_Text KnightSwordButtonText;
    public TMP_Text HeroSwordButtonText;
    public TMP_Text MonsterDaggerButtonText;
    public TMP_Text BossAxeButtonText;

    public GameObject DERSHOP;
    public GameObject Banner;
    


    // Start is called before the first frame update
    void Start()
    {
        LoadBasicSword(true);
        LoadKnightSword(true);
        LoadHeroSword(true);
        LoadMonsterDagger(true);
        LoadBossAxt(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OpenBlackSmith()
    {

        DERSHOP.SetActive(true);

        Banner.SetActive(false);

        if (SaveGame.Exists("Blacksmith") == false)
        {
            SaveGame.Save<bool>("Blacksmith", true);
            // Erster Start

            if(SaveGame.Exists("Weapon") == false)
            {
                EquipBasicSword();
            }

            else
            {
                // Erster Start, aber bereits eine Waffe vorhanden
                // Waffe wird nicht überschrieben

                string Equipmethode = "Equip" + SaveGame.Load<string>("Weapon");

                Invoke(Equipmethode, 0.05f);
                
                
            }
            
            

        }

            // Rechte Seite Update

            BasicSwordLvl.text = "----Level " + SaveGame.Load<int>("BasicSword") + "----";
        if (SaveGame.Exists("KnightSword"))
        {
            KnightSwordLvl.text = "----Level " + SaveGame.Load<int>("KnightSword") + "----";
            KnightSwordButtonText.text = "Equip";
            Debug.LogWarning("Sw1");
        }
        else
        {
            KnightSwordLvl.text = "----Level 1-----";
        }

        // Generiert


        // HeroSword
        if (SaveGame.Exists("HeroSword"))
        {
            HeroSwordLvl.text = "----Level " + SaveGame.Load<int>("HeroSword") + "----";
            HeroSwordButtonText.text = "Equip";
            Debug.LogWarning("Sw2");

        }
        else
        {
            HeroSwordLvl.text = "----Level 1----";
        }

        // MonsterDagger
        if (SaveGame.Exists("MonsterDagger"))
        {
            MonsterDaggerLvl.text = "----Level " + SaveGame.Load<int>("MonsterDagger") + "----";
            MonsterDaggerButtonText.text = "Equip";
            Debug.LogWarning("Sw3");

        }
        else
        {
            MonsterDaggerLvl.text = "----Level 1----";
        }

        // BossAxt
        if (SaveGame.Exists("BossAxe"))
        {
            BossAxeLvl.text = "----Level " + SaveGame.Load<int>("BossAxe") + "----";
            BossAxeButtonText.text = "Equip";
            Debug.LogWarning("Sw4");

        }
        else
        {
            BossAxeLvl.text = "----Level 1----";
        }





    }







    public void CloseBlackSmith()
    {

        DERSHOP.SetActive(false);

    }


    public void UpgradeWeapon()
    {
        string methode = "Upgrade" + SaveGame.Load<string>("Weapon");
        Invoke(methode, 0.05f);
        Invoke("OpenBlackSmith", 0.1f);
        
    }



    public void EquipBasicSword()
    {
        SaveGame.Save<string>("Weapon", "BasicSword");

        PreviewImage.sprite = weaponSprites[0];
        if(SaveGame.Load<int>("BasicSword") < 3)
        PreviewWeaponUpgradeCost.text = "Upgrade " + 20 + " Souls";
        else
            PreviewWeaponUpgradeCost.text = "Maxed";

        PreviewWeaponDamage.text = BasicSword.weaponDamage.ToString() + " Damage \n" + "1.5 Speed";
        PreviewWeaponLvl.text = "Level " + SaveGame.Load<int>("BasicSword");


    }

    public void EquipKnightSword()
    {
        
        if (SaveGame.Exists("KnightSword"))
        {
            SaveGame.Save<string>("Weapon", "KnightSword");



            PreviewImage.sprite = weaponSprites[1];
        if(SaveGame.Load<int>("KnightSword") < 3)
            PreviewWeaponUpgradeCost.text = "Upgrade " + 40 + " Souls";
        else
                PreviewWeaponUpgradeCost.text = "Maxed";

            PreviewWeaponDamage.text = KnightSword.weaponDamage.ToString() + " Damage \n"  + "1.25 Speed";
            PreviewWeaponLvl.text = "Level " + SaveGame.Load<int>("KnightSword");


        }
    
    }

    public void EquipHeroSword()
    {
        if (SaveGame.Exists("HeroSword"))
        {
            SaveGame.Save<string>("Weapon", "HeroSword");


            PreviewImage.sprite = weaponSprites[2]; 
            if (SaveGame.Load<int>("HeroSword") < 3)
                PreviewWeaponUpgradeCost.text = "Upgrade " + 60 + " Souls";
            else
                PreviewWeaponUpgradeCost.text = "Maxed";
            PreviewWeaponDamage.text = HeroSword.weaponDamage.ToString() + " Damage \n" + "1.0 Speed";

            PreviewWeaponLvl.text = "Level " + SaveGame.Load<int>("HeroSword");

        }
    }

    public void EquipMonsterDagger()
    {
        if (SaveGame.Exists("MonsterDagger"))
        {
            SaveGame.Save<string>("Weapon", "MonsterDagger");


            PreviewImage.sprite = weaponSprites[3];
            if (SaveGame.Load<int>("MonsterDagger") < 3)
                PreviewWeaponUpgradeCost.text = "Upgrade " + 60 + " Souls";
            else
                PreviewWeaponUpgradeCost.text = "Maxed";
            PreviewWeaponDamage.text = MonsterDagger.weaponDamage.ToString() + " Damage \n" + "0.4 Speed";

            PreviewWeaponLvl.text = "Level " + SaveGame.Load<int>("MonsterDagger");



        }

        // Kaufen

        else if (buyShop.souls >= 100)
        {
            buyShop.souls -= 100;
            buyShop.SaveSouls();
            SaveGame.Save<int>("MonsterDagger", 1);
            MonsterDaggerButtonText.text = "Equip";
        }

    }

    public void EquipBossAxe()
    {
        if (SaveGame.Exists("BossAxe"))
        {
            SaveGame.Save<string>("Weapon", "BossAxe");


            PreviewImage.sprite = weaponSprites[4];
            if (SaveGame.Load<int>("BossAxe") < 3)
                PreviewWeaponUpgradeCost.text = "Upgrade " + 60 + " Souls";
            else
                PreviewWeaponUpgradeCost.text = "Maxed";

            PreviewWeaponDamage.text = BossAxe.weaponDamage.ToString() + " Damage \n" + "3.5 Speed";

            PreviewWeaponLvl.text = "Level " + SaveGame.Load<int>("BossAxe");



        }
        // kaufen

        else if(buyShop.souls >= 200)
        {
            buyShop.souls -= 200;
            buyShop.SaveSouls();
            SaveGame.Save<int>("BossAxe", 1);
            BossAxeButtonText.text = "Equip";
        }

    }


    public void UpgradeBasicSword()
    {
        if (buyShop.souls >= 20 && SaveGame.Load<int>("BasicSword") == 1)
        {
            SaveGame.Save<int>("BasicSword", 2);
            buyShop.souls -= 20;

            buyShop.SaveSouls();   
        }
        else if (buyShop.souls >= 20 && SaveGame.Load<int>("BasicSword") == 2)
        {
            SaveGame.Save<int>("BasicSword", 3);
            buyShop.souls -= 20;

            buyShop.SaveSouls();   
        }
        LoadBasicSword(false);
    }

    public void LoadBasicSword(bool TrueLoad)
    {
        if(SaveGame.Load<int>("BasicSword") == 2)
        {
            BasicSword.weaponDamage = 12;
        }
        if(SaveGame.Load<int>("BasicSword") == 3)
        {
            BasicSword.weaponDamage = 16;
        }
        if (TrueLoad == false)
            EquipBasicSword();
    }


    // AI


    public void UpgradeKnightSword()
    {
        if (buyShop.souls >= 40 && SaveGame.Load<int>("KnightSword") == 1)
        {
            buyShop.souls -= 40;
            SaveGame.Save<int>("KnightSword", 2);
            buyShop.SaveSouls();
        }
        else if(buyShop.souls >= 40 && SaveGame.Load<int>("KnightSword") == 2)
        {
            SaveGame.Save<int>("KnightSword", 3);
            buyShop.souls -= 40;
            buyShop.SaveSouls();
        }
        LoadKnightSword(false);
    }

    public void LoadKnightSword(bool TrueLoad)
    {
        if (SaveGame.Load<int>("KnightSword") == 2)
        {
            KnightSword.weaponDamage = 14;
        }
        else if(SaveGame.Load<int>("KnightSword") == 3)
        {
            KnightSword.weaponDamage = 16;
        }
        if (TrueLoad == false)
            EquipKnightSword();
    }

    public void UpgradeHeroSword()
    {
        if (buyShop.souls >= 60 && SaveGame.Load<int>("HeroSword") == 1)
        {
            SaveGame.Save<int>("HeroSword", 2);
            buyShop.souls -= 60;
            buyShop.SaveSouls();
        }
        else if(buyShop.souls >= 60 && SaveGame.Load<int>("HeroSword") == 2)
        {
            SaveGame.Save<int>("HeroSword", 3);
            buyShop.souls -= 60;
            buyShop.SaveSouls();
        }
        LoadHeroSword(false);
    }

    public void LoadHeroSword(bool TrueLoad)
    {
        if (SaveGame.Load<int>("HeroSword") == 2)
        {
            HeroSword.weaponDamage = 18;
        }
        else if(SaveGame.Load<int>("HeroSword") == 3)
        {
            HeroSword.weaponDamage = 20;
        }
        if (TrueLoad == false)
            EquipHeroSword();
    }

    public void UpgradeMonsterDagger()
    {
        if (buyShop.souls >= 60 && SaveGame.Load<int>("MonsterDagger") == 1)
        {
            SaveGame.Save<int>("MonsterDagger", 2);
            buyShop.souls -= 60;
            buyShop.SaveSouls();
        }
        else if(buyShop.souls >= 60 && SaveGame.Load<int>("MonsterDagger") == 2)
        {
            SaveGame.Save<int>("MonsterDagger", 3);
            buyShop.souls -= 60;
            buyShop.SaveSouls();
        }
        LoadMonsterDagger(false);
    }

    public void LoadMonsterDagger(bool TrueLoad)
    {
        if (SaveGame.Load<int>("MonsterDagger") == 2)
        {
            MonsterDagger.weaponDamage = 9;
        }
        else if(SaveGame.Load<int>("MonsterDagger") == 3)
        {
            MonsterDagger.weaponDamage = 11;
        }
        if (TrueLoad = false)
            EquipMonsterDagger();
    }

    public void UpgradeBossAxe()
    {
        if (buyShop.souls >= 60 && SaveGame.Load<int>("BossAxe") == 1)
        {
            SaveGame.Save<int>("BossAxe", 2);
            buyShop.souls -= 60;
            buyShop.SaveSouls();
        }
        else if(buyShop.souls >= 60 && SaveGame.Load<int>("BossAxe") == 2)
        {
            SaveGame.Save<int>("BossAxe", 3);
            buyShop.souls -= 60;
            buyShop.SaveSouls();
        }
        
            LoadBossAxt(false);
    }

    public void LoadBossAxt(bool TrueLoad)
    {
        if (SaveGame.Load<int>("BossAxe") == 2)
        {
            BossAxe.weaponDamage = 40;
        }
        if (SaveGame.Load<int>("BossAxe") == 3)
        {
            BossAxe.weaponDamage = 60;
        }
        if(TrueLoad == false)
        EquipBossAxe();
    }







}
