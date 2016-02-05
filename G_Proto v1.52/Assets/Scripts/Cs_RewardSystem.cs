using UnityEngine;
using System.Collections;

public enum AchievementTypes
{
    HighScore,
    LongestLife,
}

/*******************************************************************************
filename    Cs_RewardSystem.cpp
author      Christopher Christensen
DP email    C.Christensen@digipen.edu
GAM Ver.    1.3

Brief Description:
  This program saves (to file), loads & manipulates various rewards.
  This includes achievements & leaderboards.
  
*******************************************************************************/
public class Cs_RewardSystem : MonoBehaviour
{
    uint ui_HighScore;
    uint ui_HighScore_LongestLife;

	// Use this for initialization
	void Start ()
    {
        LoadRewardsFromFile();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    /*******************************************************************************
        Function: LoadRewardsFromFile

     Description: Grabs the saved achievements

          Inputs: None

         Outputs: None
    *******************************************************************************/
    void LoadRewardsFromFile()
    {
        ui_HighScore =              (uint)PlayerPrefs.GetInt("HighScore");
        ui_HighScore_LongestLife =  (uint)PlayerPrefs.GetInt("HighScore_LongestLife");
    }

    /*******************************************************************************
        Function: SaveRewardsToFile

     Description: Saves the achievements to a file

          Inputs: None

         Outputs: None
    *******************************************************************************/
    void SaveRewardsToFile()
    {
        PlayerPrefs.SetInt("HighScore",             (int)ui_HighScore);
        PlayerPrefs.SetInt("HighScore_LongestLife", (int)ui_HighScore_LongestLife);

        print("Saving values: " + ui_HighScore + " " + ui_HighScore_LongestLife);


    }

    /*******************************************************************************
        Function: SetRewardInformation

     Description: Sets the value of a reward/achievement

          Inputs: achievementType_ (enum) - Gets the type of achievement
                  value_ (int)            - Receives the int value to set

         Outputs: None
    *******************************************************************************/
    public void SetRewardInformation(AchievementTypes achievementType_, int value_)
    {
        // Only messes with rewards that are NUMBERS
        if (achievementType_ == AchievementTypes.HighScore)     if(value_ > ui_HighScore) ui_HighScore = (uint)value_;
        if (achievementType_ == AchievementTypes.LongestLife)   if(value_ > ui_HighScore_LongestLife)    ui_HighScore_LongestLife = (uint)value_;

        // Store all data
        SaveRewardsToFile();
    }

    /*******************************************************************************
        Function: SetRewardInformation

     Description: Sets the value of a reward/achievement

          Inputs: achievementType_ (enum) - Gets the type of achievement
                  value_ (uint)            - Receives the uint value to set

         Outputs: None
    *******************************************************************************/
    public void SetRewardInformation(AchievementTypes achievementType_, uint value_)
    {
        // Only messes with rewards that are NUMBERS
        if (achievementType_ == AchievementTypes.HighScore) if(value_ > ui_HighScore) ui_HighScore = value_;
        if (achievementType_ == AchievementTypes.LongestLife) if(value_ > ui_HighScore_LongestLife) ui_HighScore_LongestLife = value_;

        // Store all data
        SaveRewardsToFile();
    }

    /*******************************************************************************
        Function: SetRewardInformation

     Description: Sets the value of a reward/achievement

          Inputs: achievementType_ (enum) - Gets the type of achievement
                  b_IsTrue_ (bool)         - Receives a bool to lock/unlock

         Outputs: None
    *******************************************************************************/
    public void SetRewardInformation(AchievementTypes achievementType_, bool b_IsTrue_)
    {
        // Only messes with rewards that are BOOLEANS (Did the player accomplish something)

        // Store all data
        SaveRewardsToFile();
    }

    /*******************************************************************************
        Function: SetRewardInformation

     Description: Sets the value of a reward/achievement

          Inputs: achievementType_ (enum) - Gets the type of achievement
                  value_ (string)            - Receives the int value to set

         Outputs: None
    *******************************************************************************/
    public void SetRewardInformation(AchievementTypes achievementType_, string value_)
    {
        // Only messes with rewards that are STRINGS (Ex: Player Names)

        // Store all data
        SaveRewardsToFile();
    }

    /*******************************************************************************
    Function: ResetRewardValues

 Description: Resets all achievement scores

      Inputs: None

     Outputs: None
*******************************************************************************/
    public void ResetRewardValues()
    {
        ui_HighScore = 0;
        ui_HighScore_LongestLife = 0;

        SaveRewardsToFile();
    }
}
