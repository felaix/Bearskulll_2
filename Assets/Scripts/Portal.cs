using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int PortalID;
    
    [SerializeField] private int _goToSceneID;

    public GameObject Menu1;
    public GameObject Menu2;

    public bool KeyRequired = false;
    public Inventory Player;
    public Archievments.Level Level = new Archievments.Level();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !KeyRequired)
        {
            StartCoroutine(Transition());
        }

        else if (other.tag == "Player" && KeyRequired)
        {
            if(Player.key > 0)
            {
                Player.key--;

                StartCoroutine(Transition());

            }


        }


    }

    private IEnumerator Transition()
    {

        if (SaveGame.Exists("Diffi"))
        {
            if (SaveGame.Load<string>("Diffi") == "Normal")
            {
              //  Archievments.Instance.MadeProgress(Archievments.Diffic.Normal, Level);

                SaveGame.Save<bool>("Normal" + Level.ToString(), true);

            }
            if (SaveGame.Load<string>("Diffi") == "Easy")
            {
                SaveGame.Save<bool>("Easy" + Level.ToString(), true);

            }
            if (SaveGame.Load<string>("Diffi") == "Hard")
            {
                SaveGame.Save<bool>("Hard" + Level.ToString(), true);

            }



        }


        DontDestroyOnLoad(gameObject);


        SaveSystem.instance.endsave();

        SaveManager.instance.SaveStats();
        if (Menu1 != null)
        {

         //   Destroy(Menu1);
          //  Destroy(Menu2);
        }
        yield return SceneManager.LoadSceneAsync(_goToSceneID);

        yield return new WaitForSeconds(1);
        SaveManager.instance.LoadStats();
        Destroy(gameObject);
    }
   
}
