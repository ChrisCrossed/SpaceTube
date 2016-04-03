using UnityEngine;
using System.Collections;

public enum AchievementTypes
{
    HighScore,
    LongestLife,
}

public class ScoreboardInfo
{
    public string Name_1st;
    public string Name_2nd;
    public string Name_3rd;
    public string Name_4th;
    public string Name_5th;
    public uint Score_1st;
    public uint Score_2nd;
    public uint Score_3rd;
    public uint Score_4th;
    public uint Score_5th;
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

    string Name_1st = "Alpha";
    string Name_2nd = "Beta";
    string Name_3rd = "Charlie";
    string Name_4th = "Echo";
    string Name_5th = "Delta";
    uint Score_1st = 1000;
    uint Score_2nd = 900;
    uint Score_3rd = 800;
    uint Score_4th = 700;
    uint Score_5th = 600;

    // Packages the info to pass externally
    ScoreboardInfo scoreboardInfo;

    // Use this for initialization
    void Start()
    {
        scoreboardInfo = new ScoreboardInfo();

        LoadRewardsFromFile();
    }

    // Update is called once per frame
    void Update()
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
        // ui_HighScore =              (uint)PlayerPrefs.GetInt("HighScore");
        // ui_HighScore_LongestLife =  (uint)PlayerPrefs.GetInt("HighScore_LongestLife");

        if (PlayerPrefs.HasKey("Name_1st"))
        {
            scoreboardInfo.Name_1st = PlayerPrefs.GetString("Name_1st");
            scoreboardInfo.Name_2nd = PlayerPrefs.GetString("Name_2nd");
            scoreboardInfo.Name_3rd = PlayerPrefs.GetString("Name_3rd");
            scoreboardInfo.Name_4th = PlayerPrefs.GetString("Name_4th");
            scoreboardInfo.Name_5th = PlayerPrefs.GetString("Name_5th");

            scoreboardInfo.Score_1st = (uint)PlayerPrefs.GetInt("Score_1st");
            scoreboardInfo.Score_2nd = (uint)PlayerPrefs.GetInt("Score_2nd");
            scoreboardInfo.Score_3rd = (uint)PlayerPrefs.GetInt("Score_3rd");
            scoreboardInfo.Score_4th = (uint)PlayerPrefs.GetInt("Score_4th");
            scoreboardInfo.Score_5th = (uint)PlayerPrefs.GetInt("Score_5th");
        }
        else
        {
            scoreboardInfo.Name_1st = Name_1st;
            scoreboardInfo.Name_2nd = Name_2nd;
            scoreboardInfo.Name_3rd = Name_3rd;
            scoreboardInfo.Name_4th = Name_4th;
            scoreboardInfo.Name_5th = Name_5th;

            scoreboardInfo.Score_1st = Score_1st;
            scoreboardInfo.Score_2nd = Score_2nd;
            scoreboardInfo.Score_3rd = Score_3rd;
            scoreboardInfo.Score_4th = Score_4th;
            scoreboardInfo.Score_5th = Score_5th;

        }

        // print("Load Rewards");
        SaveRewardsToFile();
    }

    /*******************************************************************************
        Function: SaveRewardsToFile

     Description: Saves the achievements to a file

          Inputs: None

         Outputs: None
    *******************************************************************************/
    void SaveRewardsToFile()
    {
        // PlayerPrefs.SetInt("HighScore",             (int)ui_HighScore);
        // PlayerPrefs.SetInt("HighScore_LongestLife", (int)ui_HighScore_LongestLife);

        //print("Saving values: " + scoreboardInfo.Name_1st);

        PlayerPrefs.SetString("Name_1st", scoreboardInfo.Name_1st);
        PlayerPrefs.SetString("Name_2nd", scoreboardInfo.Name_2nd);
        PlayerPrefs.SetString("Name_3rd", scoreboardInfo.Name_3rd);
        PlayerPrefs.SetString("Name_4th", scoreboardInfo.Name_4th);
        PlayerPrefs.SetString("Name_5th", scoreboardInfo.Name_5th);

        PlayerPrefs.SetInt("Score_1st", (int)scoreboardInfo.Score_1st);
        PlayerPrefs.SetInt("Score_2nd", (int)scoreboardInfo.Score_2nd);
        PlayerPrefs.SetInt("Score_3rd", (int)scoreboardInfo.Score_3rd);
        PlayerPrefs.SetInt("Score_4th", (int)scoreboardInfo.Score_4th);
        PlayerPrefs.SetInt("Score_5th", (int)scoreboardInfo.Score_5th);
    }

    public ScoreboardInfo GetScoreboardInfo()
    {
        if(scoreboardInfo.Name_1st == null)
        {
            scoreboardInfo = new ScoreboardInfo();

            scoreboardInfo.Name_1st = Name_1st;
            scoreboardInfo.Name_2nd = Name_2nd;
            scoreboardInfo.Name_3rd = Name_3rd;
            scoreboardInfo.Name_4th = Name_4th;
            scoreboardInfo.Name_5th = Name_5th;

            scoreboardInfo.Score_1st = Score_1st;
            scoreboardInfo.Score_2nd = Score_2nd;
            scoreboardInfo.Score_3rd = Score_3rd;
            scoreboardInfo.Score_4th = Score_4th;
            scoreboardInfo.Score_5th = Score_5th;
        }

        return scoreboardInfo;
    }

    /*******************************************************************************
        Function: SetScoreInformation

     Description: Sets the value of the player's score

          Inputs: i_PlayerScore_ (int)            - Receives the int value to set

         Outputs: None
    *******************************************************************************/
    public void SetScoreOnDeath(int i_PlayerScore_)
    {
        int currPos = 6;

        // Run through the group of scoreboard scores.
        // If the score received is higher than the current lowest score, decrease the counter
        if (i_PlayerScore_ > scoreboardInfo.Score_5th) --currPos; else return;
        if (i_PlayerScore_ > scoreboardInfo.Score_4th) --currPos;
        if (i_PlayerScore_ > scoreboardInfo.Score_3rd) --currPos;
        if (i_PlayerScore_ > scoreboardInfo.Score_2nd) --currPos;
        if (i_PlayerScore_ > scoreboardInfo.Score_1st) --currPos;
        
        // Move the current score to the position below it
        for(int i = 5; i >= currPos; --i)
        {
            if(i == currPos)
            {
                if (i == 1) scoreboardInfo.Score_1st = (uint)i_PlayerScore_;
                if (i == 2) scoreboardInfo.Score_2nd = (uint)i_PlayerScore_;
                if (i == 3) scoreboardInfo.Score_3rd = (uint)i_PlayerScore_;
                if (i == 4) scoreboardInfo.Score_4th = (uint)i_PlayerScore_;
                if (i == 5) scoreboardInfo.Score_5th = (uint)i_PlayerScore_;
            }
            else
            {
                if (i == 2) scoreboardInfo.Score_2nd = scoreboardInfo.Score_1st;
                if (i == 3) scoreboardInfo.Score_3rd = scoreboardInfo.Score_2nd;
                if (i == 4) scoreboardInfo.Score_4th = scoreboardInfo.Score_3rd;
                if (i == 5) scoreboardInfo.Score_5th = scoreboardInfo.Score_4th;
            }
        }

        // Save the scores to the scoreboard
        SaveRewardsToFile();
    }

    #region SetRewardInformation Overloads
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
        if (achievementType_ == AchievementTypes.HighScore) if (value_ > ui_HighScore) ui_HighScore = (uint)value_;
        if (achievementType_ == AchievementTypes.LongestLife) if (value_ > ui_HighScore_LongestLife) ui_HighScore_LongestLife = (uint)value_;

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
        if (achievementType_ == AchievementTypes.HighScore) if (value_ > ui_HighScore) ui_HighScore = value_;
        if (achievementType_ == AchievementTypes.LongestLife) if (value_ > ui_HighScore_LongestLife) ui_HighScore_LongestLife = value_;

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
    #endregion

    /*******************************************************************************
    Function: SetScoreboardInformation

 Description: Receives the new Scoreboard information from external sources

      Inputs: scoreboardInfo_ (ScoreboardInfo) - The new scoreboardinfo

     Outputs: None
*******************************************************************************/
    public void SetScoreboardInformation(ScoreboardInfo scoreboardInfo_)
    {
        //print("New 5th: " + scoreboardInfo_.Score_5th);

        scoreboardInfo.Score_1st = scoreboardInfo_.Score_1st;
        scoreboardInfo.Score_2nd = scoreboardInfo_.Score_2nd;
        scoreboardInfo.Score_3rd = scoreboardInfo_.Score_3rd;
        scoreboardInfo.Score_4th = scoreboardInfo_.Score_4th;
        scoreboardInfo.Score_5th = scoreboardInfo_.Score_5th;

        scoreboardInfo.Name_1st = scoreboardInfo_.Name_1st;
        scoreboardInfo.Name_2nd = scoreboardInfo_.Name_2nd;
        scoreboardInfo.Name_3rd = scoreboardInfo_.Name_3rd;
        scoreboardInfo.Name_4th = scoreboardInfo_.Name_4th;
        scoreboardInfo.Name_5th = scoreboardInfo_.Name_5th;

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

        scoreboardInfo = new ScoreboardInfo();

        scoreboardInfo.Name_1st = "Alpha";
        scoreboardInfo.Name_2nd = "Beta";
        scoreboardInfo.Name_3rd = "Charlie";
        scoreboardInfo.Name_4th = "Echo";
        scoreboardInfo.Name_5th = "Delta";
        scoreboardInfo.Score_1st = 1000;
        scoreboardInfo.Score_2nd = 900;
        scoreboardInfo.Score_3rd = 800;
        scoreboardInfo.Score_4th = 700;
        scoreboardInfo.Score_5th = 600;

        //print(scoreboardInfo.Name_1st);

        SaveRewardsToFile();
    }
}
