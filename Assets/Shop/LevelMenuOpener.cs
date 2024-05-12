using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class LevelMenuOpener : MonoBehaviour
{


    public GameObject MyLevelMenu;
    public GameObject LockedMenu;
    public GameObject Green;
    public GameObject Yellow;
    public GameObject Red;

    public int Leveltoload;

    public string Levelname;
    public string PreviousLevelname;




    private void Start()
    {
        Debug.LogError(Leveltoload + " dads "+ this.gameObject.name);

        if (Levelname == "SHOP")
        {

            Green.SetActive(true);
           

            return;
        }

        else if (SaveGame.Exists(Levelname))
        {

            Green.SetActive(true);
            Yellow.SetActive(false);
            Red.SetActive(false);

        }
        else if(SaveGame.Exists(PreviousLevelname) || Leveltoload == 1) 
        {

            Green.SetActive(false);
            Yellow.SetActive(true);
            Red.SetActive(false);
        
        }


        else if(Levelname == "Level1")
        {

            Green.SetActive(false);
            Yellow.SetActive(true);
            Red.SetActive(false);

        }



        else
        {
            Green.SetActive(false);
            Yellow.SetActive(false);
            Red.SetActive(true);

        }








    }





    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag == "Player" && Red.active == false || Levelname == "SHOP")
        {
            Debug.Log("Collision2");

            MyLevelMenu.SetActive(true);
            LockedMenu.SetActive(false);


        }  
        else if (other.gameObject.tag == "Player" && Red.active == true)
        {
            Debug.Log("Collision2");
            MyLevelMenu.SetActive(true);

            LockedMenu.SetActive(true);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collision2");

            MyLevelMenu.SetActive(false);
            LockedMenu.SetActive(false);


        }
    }


    public void LoadLevel()
    {

        MapLoader.Instance.SavePlayer();

        Fader.Instance.StartCoroutine(Fader.Instance.FadeImageCoro(true));

        Invoke("LoadLevelFinal", 1.51f);

    }

    public void LoadLevelFinal()
    {
        SceneManager.LoadScene(Leveltoload);
    }



}
