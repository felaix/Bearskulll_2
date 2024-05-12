using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.SocialPlatforms.Impl;
using System;
using BayatGames.SaveGameFree;
using System.Linq;



public class SaveData
{
    public List<LevelProgress> THELIST;
}




    public class LevelProgress : Archievments
{
    public Level level;
    public Diffic difficulty;
    public bool isCleared;

    public LevelProgress(Level level, Diffic difficulty, bool isCleared)
    {
        this.level = level;
        this.difficulty = difficulty;
        this.isCleared = isCleared;
    }
}

public class Archievments : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsSorkinLevel = false;


    public enum Diffic
    {
        Easy,
        Normal,
        Hard,
        Hardplus
    }

    public enum Level
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7
    }

    
    private List<LevelProgress> progressList = new List<LevelProgress>();

    public static Archievments Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {

        //   progress = SaveGame.Load<>
        if (SaveGame.Exists("PROGRESSDATA_DONT_DELETE"))
        {
            var data = SaveGame.Load<SaveData>("PROGRESSDATA_DONT_DELETE");
            progressList = data.THELIST;
        }
        else
        {
            progressList = new List<LevelProgress>();
            var data = new SaveData();
            data.THELIST = progressList;
            SaveGame.Save<SaveData>("PROGRESSDATA_DONT_DELETE", data);

        }



        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steamworks is not initialized!");
            return;
        }

        // Fetch the user's current stats and achievements from Steam.
        // This is often done once at the start of the game.
        SteamUserStats.RequestCurrentStats();

        CheckAllAchievements();
        if (IsSorkinLevel)
        {
            UnlockAchievement("SORKIN");




            if (SaveGame.Exists("Diffi"))
            {
                if (SaveGame.Load<string>("Diffi") == "Normal")
                {
                    Archievments.Instance.MadeProgress(Diffic.Normal, Level.Level7);

                }
                if (SaveGame.Load<string>("Diffi") == "Easy")
                {
                    Archievments.Instance.MadeProgress(Diffic.Easy, Level.Level7);
                }
                if (SaveGame.Load<string>("Diffi") == "Hard")
                {
                    Archievments.Instance.MadeProgress(Diffic.Hard, Level.Level7);
                }



            }



            if (SaveGame.Load<string>("Diffi") == "Normal")
            {
               

                SaveGame.Save<bool>("NormalLevel7", true);

            }
            if (SaveGame.Load<string>("Diffi") == "Easy")
            {
                SaveGame.Save<bool>("EasyLevel7", true);

            }
            if (SaveGame.Load<string>("Diffi") == "Hard")
            {
                SaveGame.Save<bool>("HardLevel7", true);

            }

        }


    }



    public void UnlockAchievement(string ArchievmentID)
    {
        if (HasAchievement(ArchievmentID))
        {
            Debug.Log("Achievement already unlocked!");
            return;
        }

        bool success = SteamUserStats.SetAchievement(ArchievmentID);
        if (success)
        {
            // Store stats and achievements, this also makes them visible in Steam UI.
            SteamUserStats.StoreStats();
            Debug.Log("Achievement unlocked!");
        }
        else
        {
            Debug.LogError("Failed to unlock achievement.");
        }
    }

    // Check if the user has a specific achievement
    public bool HasAchievement(string ArchievmentID)
    {
        bool hasAchievement = false;
        bool success = SteamUserStats.GetAchievement(ArchievmentID, out hasAchievement);
        if (!success)
        {
            Debug.LogError("Failed to get achievement status.");
            return false;
        }
        return hasAchievement;
    }




    private void Update()
    {
        
    }



    public void IncrementStat()
    {
        int currentStatValue;
        bool gotStat = SteamUserStats.GetStat("SOULS", out currentStatValue);
        if (gotStat)
        {
            bool setStatSuccess = SteamUserStats.SetStat("SOULS", currentStatValue + 1);
            if (setStatSuccess)
            {
                SteamUserStats.StoreStats();
                Debug.Log("Stat incremented!");

                if (currentStatValue + 1 >= 1000)
                {
                    UnlockAchievement("Souls_Score");
                }
            }
            else
            {
                Debug.LogError("Failed to set stat value.");
            }
        }
        else
        {
            Debug.LogError("Failed to get stat value.");
        }
    }


    //SO NEXT














    public void MadeProgress(Diffic difficulty, Level level)
    {
        var existingProgress = progressList.FirstOrDefault(p => p.level == level && p.difficulty == difficulty);
        if (existingProgress != null && existingProgress.level == level && existingProgress.difficulty == difficulty)
        {
            // Update existing entry
            existingProgress.isCleared = true;
        }
        else
        {
            // Add new entry
            progressList.Add(new LevelProgress(level, difficulty, true));
        }


        CheckAllAchievements();

        SaveData Saveing = new SaveData();
        Saveing.THELIST = progressList;
        SaveGame.Save<SaveData>("PROGRESSDATA_DONT_DELETE", Saveing);



    }

    public bool IsCleared(Diffic difficulty, Level level)
    {
        var existingProgress = progressList.FirstOrDefault(p => p.level == level && p.difficulty == difficulty);
        if (existingProgress != null)
            return existingProgress.isCleared;
        else
            return false;

    }

    public bool CheckAchievementForDifficulty(Diffic difficulty)
    {
        

        if(difficulty == Diffic.Easy)
        {

            if (SaveGame.Exists("EasyLevel1") && SaveGame.Exists("EasyLevel2") && SaveGame.Exists("EasyLevel3") && SaveGame.Exists("EasyLevel4") && SaveGame.Exists("EasyLevel5") && SaveGame.Exists("EasyLevel6") && SaveGame.Exists("EasyLevel7"))
            { 
                return true;
            }

            if(SaveGame.Exists("NormalLevel1") && SaveGame.Exists("NormalLevel2") && SaveGame.Exists("NormalLevel3") && SaveGame.Exists("NormalLevel4") && SaveGame.Exists("NormalLevel5") && SaveGame.Exists("NormalLevel6") && SaveGame.Exists("NormalLevel7"))
            {
                return true;
            }

            if (SaveGame.Exists("HardLevel1") && SaveGame.Exists("HardLevel2") && SaveGame.Exists("HardLevel3") && SaveGame.Exists("HardLevel4") && SaveGame.Exists("HardLevel5") && SaveGame.Exists("HardLevel6") && SaveGame.Exists("HardLevel7"))
            {
                return true;
            }

        }









        if (difficulty == Diffic.Normal)
        {


            if (SaveGame.Exists("NormalLevel1") && SaveGame.Exists("NormalLevel2") && SaveGame.Exists("NormalLevel3") && SaveGame.Exists("NormalLevel4") && SaveGame.Exists("NormalLevel5") && SaveGame.Exists("NormalLevel6") && SaveGame.Exists("NormalLevel7"))
            {
                return true;
            }

            if (SaveGame.Exists("HardLevel1") && SaveGame.Exists("HardLevel2") && SaveGame.Exists("HardLevel3") && SaveGame.Exists("HardLevel4") && SaveGame.Exists("HardLevel5") && SaveGame.Exists("HardLevel6") && SaveGame.Exists("HardLevel7"))
            {
                return true;
            }

        }




        if (difficulty == Diffic.Hard)
        {



            if (SaveGame.Exists("HardLevel1") && SaveGame.Exists("HardLevel2") && SaveGame.Exists("HardLevel3") && SaveGame.Exists("HardLevel4") && SaveGame.Exists("HardLevel5") && SaveGame.Exists("HardLevel6") && SaveGame.Exists("HardLevel7"))
            {
                return true;
            }

        }


        return (false);
    }

    public void CheckAllAchievements()
    {
        if (CheckAchievementForDifficulty(Diffic.Easy))
            UnlockAchievement("ALL_EASY");

        if (CheckAchievementForDifficulty(Diffic.Normal))
            UnlockAchievement("ALL_NORMAL");

        if (CheckAchievementForDifficulty(Diffic.Hard))
            UnlockAchievement("ALL_HARD");

        if (CheckAchievementForDifficulty(Diffic.Hardplus))
            UnlockAchievement("ALL_HARD_BASIC");

    }








}
