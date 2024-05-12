using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BayatGames.SaveGameFree;

public class MenuManager : MonoBehaviour
{

    public void ExitGame() //back to menu
    {

        if (SceneManager.GetActiveScene().name == "Map")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
    
    
    }
    public void StartGame() //back to menu
    {
        if (!SaveGame.Exists("Testes12d"))
        {
            SaveGame.Save("Testes12d", 1);
            SaveManager.instance.ResetSave();
            SceneManager.LoadScene(1);
        }

        else
        {
            SceneManager.LoadScene(7);

        }

    }
    
    

    public void QuitGame()
    {
        Application.Quit();
    }

}
