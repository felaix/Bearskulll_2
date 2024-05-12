using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using TMPro;

public class BuyShop : MonoBehaviour
{
    public int souls = 0;
    public TMP_Text soulText;
    bool BigBagOwned = false;
    bool SuperBagOwned = false;
    public TMP_Text BigBagText;
    public TMP_Text SuperBagText;
    public GameObject ShopWindow;
    public GameObject PanelMenu;

    // Start is called before the first frame update
    void Start()
    {
        souls = SaveGame.Load<int>("Souls");

        if (SaveGame.Exists("BigBag"))
        {
            BigBagOwned = SaveGame.Load<bool>("BigBag");
            BigBagText.text = "Owned";
        }
        if (SaveGame.Exists("SuperBag"))
        {
            SuperBagOwned = SaveGame.Load<bool>("SuperBag");
            SuperBagText.text = "Owned";
        }

    }

    // Update is called once per frame
    void Update()
    {
        soulText.text = souls.ToString();
    }
    public void OpenShop()
    {
        ShopWindow.SetActive(true);
        PanelMenu.SetActive(false);
    }
    public void CloseShop()
    {
        ShopWindow.SetActive(false);
    }

    public void BuyBigBag()
    {
        if (souls >= 75 && !BigBagOwned)
        {
            souls -= 75;
            BigBagOwned = true;
            BigBagText.text = "Owned";
            SaveGame.Save<int>("Souls", souls);
            SaveGame.Save<bool>("BigBag", true);
        }
    }

    public void BuySuperBag()
    {
        if (souls >= 150 && !SuperBagOwned)
        {
            souls -= 150;
            SuperBagText.text = "Owned";
            SuperBagOwned = true;
            SaveGame.Save<int>("Souls", souls);
            SaveGame.Save<bool>("SuperBag", true);
            Archievments.Instance.UnlockAchievement("BEST_BAG");



            BigBagOwned = true;
            BigBagText.text = "Owned";
            SaveGame.Save<bool>("BigBag", true);
        }
    }

    public void SaveSouls()
    {
        SaveGame.Save<int>("Souls", souls);
    }


}
