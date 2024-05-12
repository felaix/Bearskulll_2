using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{

    public Image DifficultyImageOneEASY;
    public Image DifficultyImageTwoNORMAL;
    public Image DifficultyImageThreeHARD;

    bool alreadySelected = false;

    public void SelectDifficulty()
    {
        if (alreadySelected) return;
        SaveGame.Save<string>("Diffi", "Normal");


    }

    public void SelectNormal()
    {
        alreadySelected = true;
        SaveGame.Save<string>("Diffi", "Normal");
        DifficultyImageOneEASY.rectTransform.localScale = new Vector3(1, 1, 1);
        DifficultyImageTwoNORMAL.rectTransform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
        DifficultyImageThreeHARD.rectTransform.localScale = new Vector3(1, 1, 1);
    }
    public void SelectEasy()
    {
        alreadySelected = true;

        SaveGame.Save<string>("Diffi", "Easy");
        DifficultyImageOneEASY.rectTransform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
        DifficultyImageTwoNORMAL.rectTransform.localScale = new Vector3(1, 1, 1);
        DifficultyImageThreeHARD.rectTransform.localScale = new Vector3(1, 1, 1);
    }
    public void SelectHard()
    {
        alreadySelected = true;

        SaveGame.Save<string>("Diffi", "Hard");
        DifficultyImageOneEASY.rectTransform.localScale = new Vector3(1, 1, 1);
        DifficultyImageTwoNORMAL.rectTransform.localScale = new Vector3(1, 1, 1);
        DifficultyImageThreeHARD.rectTransform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
    }



}
