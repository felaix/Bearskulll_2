using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;
using UnityEditor;
using Unity.VisualScripting;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public GameObject SaveUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    [Header("Inventar")]
    public GameObject player;
    public int HeartSave, EnergySave;
    public Weapon WeaponSave;
    public Weapon Shield;

    public int Souls;
    public Text SoulsCount;

    private bool restarting;

    private void Update()
    {
        if (player != null) { 
            HeartSave = player.GetComponent<Inventory>().heart;
            EnergySave = player.GetComponent<Inventory>().energy;
                            }
        else
            player = GameObject.FindGameObjectsWithTag("Player")[0];

        if(player.GetComponent<Health>().isDead && !restarting)
        {
            StartCoroutine(Restart());
        }

        SoulsCount.text = Souls.ToString();
    }

    public void addSoul()
    {
       StartCoroutine(addingSouls());
        SaveGame.Save<int>("Souls", (Souls + 1) );
    }
    private void Start()
    {
        Souls = SaveGame.Load<int>("Souls");
    }

    IEnumerator addingSouls()
    {
        
      
        SaveUI.GetComponent<Animator>().SetBool("Soul", true);
        yield return new WaitForSeconds(2);
        Souls++;
        yield return new WaitForSeconds(2);
        SaveUI.GetComponent<Animator>().SetBool("Soul", false);

    }


    public void SaveStats()
    {
        
        string heaCount = HeartSave.ToString();
        string enrCount = EnergySave.ToString();

        string save =  heaCount + "|" + enrCount;

        PlayerPrefs.SetString("PlayerSave", save);
    }

    public void LoadStats()
    {
        string load = string.Empty;
        load = PlayerPrefs.GetString("PlayerSave", "0|0");
        string[] splitLoad = load.Split('|');
      
        player.GetComponent<Fighter>().EquipWeapon(WeaponSave);
        if (WeaponSave.GetName() == "Shield" && Shield == null) Shield = WeaponSave;
        if (Shield != null) player.GetComponent<Fighter>().EquipWeapon(Shield);
       
        //tränke zuweisen
        player.GetComponent<Inventory>().heart = int.Parse(splitLoad[0]);
        player.GetComponent<Inventory>().energy = int.Parse(splitLoad[1]);
    }

    public void ResetSave()
    {
        WeaponSave = null;
        PlayerPrefs.DeleteKey("PlayerSave");
    }


    IEnumerator Restart()
    {
        restarting = true;
        yield return new WaitForSeconds(2);
        SaveStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(2);
        LoadStats();
        restarting = false;
    }

    public void OnLevelWasLoaded(int level)
    {
        Souls = SaveGame.Load<int>("Souls");
    }




}
