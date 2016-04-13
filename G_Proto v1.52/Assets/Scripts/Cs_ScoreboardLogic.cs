/*******************************  SpaceTube  *********************************
Author: Christopher Christensen
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   Cs_ScoreboardLogic.cs

Description:
  Handles any logic with displaying and using the scoreboard

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XInputDotNetPure;

public enum ScoreboardState
{
    Start,
    Display,
    ReceiveName,
    None
}

public class Cs_ScoreboardLogic : MonoBehaviour
{
    GameObject[] NameGroup = new GameObject[5];
    GameObject[] ScoreGroup = new GameObject[5];
    string[] NameList = new string[5];
    string[] ScoreList = new string[5];

    int[] NameLength = new int[5];
    int[] NameLength_Curr = new int[5];
    int[] ScoreLength = new int[5];
    int[] ScoreLength_Curr = new int[5];

    float f_Timer = -1f;
    float f_LetterTimer = -1f;
    int i_NameCounter = -1;
    int i_ScoreCounter = -1;

    bool b_IsNewScore;
    int i_ScorePos;
    ScoreboardState scoreboardState;
    string s_PlayerName;
    GameObject VictoryTextObject;
    GameObject HighScoreNameObject;
    float f_flashTimer;

    public GameObject MainMenu;
    float f_ScoreboardTimer;

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    GameObject go_DPad;
    GameObject go_LStick;
    GameObject go_Keyboard;

    // Use this for initialization
    void Start()
    {
        s_PlayerName = "";
        playerIndex = PlayerIndex.One;

        go_DPad = GameObject.Find("Img_DPad");
        go_LStick = GameObject.Find("Img_LStick");
        go_Keyboard = GameObject.Find("Img_Keyboard");

        // StartShowScoreboard();
        // scoreboardState = ScoreboardState.Start;
    }

    public void SetScoreboardState(ScoreboardState scoreboardState_)
    {
        scoreboardState = scoreboardState_;
    }

    public void SetPlayerScore(int playerScore_)
    {
        i_ScorePos = 0;

        StartShowScoreboard();

        if(scoreboardState == ScoreboardState.Start)
        {
            // We already determined that the player's score is greater than 5th place
            // Determine what position the score is
            gameObject.GetComponent<Cs_RewardSystem>().LoadRewardsFromFile();
            ScoreboardInfo scoreInfo = gameObject.GetComponent<Cs_RewardSystem>().GetScoreboardInfo();

            if (playerScore_ > (int)scoreInfo.Score_5th) i_ScorePos = 5;
            if (playerScore_ > (int)scoreInfo.Score_4th) i_ScorePos = 4;
            if (playerScore_ > (int)scoreInfo.Score_3rd) i_ScorePos = 3;
            if (playerScore_ > (int)scoreInfo.Score_2nd) i_ScorePos = 2;
            if (playerScore_ > (int)scoreInfo.Score_1st) i_ScorePos = 1;

            //print("Player Score Position: " + i_ScorePos);
            //print("Player Score: " + playerScore_);
            gameObject.GetComponent<Cs_RewardSystem>().SetScoreOnDeath(playerScore_);

            // Ask for the player's name
            scoreboardState = ScoreboardState.ReceiveName;
        }
    }

    public void StartShowScoreboard()
    {
        // Load GameObjects
        LoadGameObjects();

        // Load names & scores
        LoadInformation();

        // Reset timers
        f_Timer = -1f;
        f_LetterTimer = -1f;
        i_NameCounter = -1;
        i_ScoreCounter = -1;
        b_IsNewScore = false;
        i_ScorePos = 0;
        s_PlayerName = "";
        f_flashTimer = 0f;
        f_ScoreboardTimer = 0f;
        HighScoreNameObject.GetComponent<Text>().color = new Color(1, 1, 1, 1);
        VictoryTextObject.GetComponent<Text>().color = new Color(1, 1, 1, 1);

        // Start all text as black
        for (int i = 0; i < 5; ++i)
        {
            NameGroup[i].GetComponent<Text>().color = new Color(0, 0, 0, 0);
            ScoreGroup[i].GetComponent<Text>().color = new Color(0, 0, 0, 0);
        }

        // Enable Scoreboard & Disable MainMenu
    }

    void LoadInformation()
    {
        // Load the score information
        gameObject.GetComponent<Cs_RewardSystem>().LoadRewardsFromFile();
        ScoreboardInfo scoreInfo = gameObject.GetComponent<Cs_RewardSystem>().GetScoreboardInfo();

        // Save the information
        NameList[0] = scoreInfo.Name_1st;
        NameList[1] = scoreInfo.Name_2nd;
        NameList[2] = scoreInfo.Name_3rd;
        NameList[3] = scoreInfo.Name_4th;
        NameList[4] = scoreInfo.Name_5th;

        ScoreList[0] = scoreInfo.Score_1st.ToString();
        ScoreList[1] = scoreInfo.Score_2nd.ToString();
        ScoreList[2] = scoreInfo.Score_3rd.ToString();
        ScoreList[3] = scoreInfo.Score_4th.ToString();
        ScoreList[4] = scoreInfo.Score_5th.ToString();

        for (int i = 0; i < 5; ++i)
        {
            NameLength[i] = NameList[i].Length;
            NameLength_Curr[i] = 0;
            ScoreLength[i] = ScoreList[i].Length;
            ScoreLength_Curr[i] = 0;

            NameGroup[i].GetComponent<Text>().text = "";
            ScoreGroup[i].GetComponent<Text>().text = "";
        }
    }

    void LoadGameObjects()
    {
        NameGroup[0] = GameObject.Find("Name_1");
        NameGroup[1] = GameObject.Find("Name_2");
        NameGroup[2] = GameObject.Find("Name_3");
        NameGroup[3] = GameObject.Find("Name_4");
        NameGroup[4] = GameObject.Find("Name_5");

        ScoreGroup[0] = GameObject.Find("Score_1");
        ScoreGroup[1] = GameObject.Find("Score_2");
        ScoreGroup[2] = GameObject.Find("Score_3");
        ScoreGroup[3] = GameObject.Find("Score_4");
        ScoreGroup[4] = GameObject.Find("Score_5");

        VictoryTextObject = GameObject.Find("VictoryTextObject");
        HighScoreNameObject = GameObject.Find("HighScoreNameObject");
    }

    void IncrementLetterVisibility(int currWord_, int currScore_)
    {
        // Starts the current length of the word at 0 and checks that it is less than the length of the actual word
        if (NameLength_Curr[currWord_] < NameLength[currWord_])
        {
            // Prints the reset text object at the current array position 
            NameGroup[currWord_].GetComponent<Text>().text = "";

            // Prints up to the current length of the word
            for (int i = 0; i < NameLength_Curr[currWord_] + 1; ++i)
            {
                // 
                char[] test = NameList[currWord_].ToCharArray(i, 1);
                NameGroup[currWord_].GetComponent<Text>().text += test[0];
            }

            ++NameLength_Curr[currWord_];
        }

        if (ScoreLength_Curr[currScore_] < ScoreLength[currScore_])
        {
            // Prints the reset text object at the current array position 
            ScoreGroup[currScore_].GetComponent<Text>().text = "";

            // Prints up to the current length of the word
            for (int i = 0; i < ScoreLength_Curr[currScore_] + 1; ++i)
            {
                // 
                char[] test = ScoreList[currScore_].ToCharArray(i, 1);
                ScoreGroup[currScore_].GetComponent<Text>().text += test[0];
            }

            ++ScoreLength_Curr[currScore_];
        }
    }

    void ShowLetters()
    {
        // Increment timer for next allowed word
        if (i_ScoreCounter < 4) f_Timer += Time.deltaTime;
        f_LetterTimer += Time.deltaTime;

        if (f_Timer >= 0.5f + i_NameCounter) ++i_NameCounter;

        if (f_Timer >= 1f + i_ScoreCounter) ++i_ScoreCounter;

        if (f_LetterTimer >= 0.05f)
        {
            for (int j = 0; j < i_ScoreCounter + 1; ++j)
            {
                for (int i = 0; i < i_NameCounter + 1; ++i)
                {
                    IncrementLetterVisibility(i, j);
                }
            }

            f_LetterTimer = 0.0f;
        }

        if (i_NameCounter >= 0) NameGroup[i_NameCounter].GetComponent<Text>().color = new Color(1, 1, 1, 1);
        if (i_ScoreCounter >= 0) ScoreGroup[i_ScoreCounter].GetComponent<Text>().color = new Color(1, 1, 1, 1);

        VictoryTextObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
        HighScoreNameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
    }

    void ScoreboardIncrementLetter()
    {
        // Preload the name if empty
        if (s_PlayerName.Length == 0) s_PlayerName = "A";
        else
        {
            // Get the last letter of the player name
            int lastLetterAsInt = s_PlayerName[s_PlayerName.Length - 1];

            // Increment the letter
            if (lastLetterAsInt < 90) lastLetterAsInt += 1; else lastLetterAsInt = 65;

            // Convert to char
            char newLastLetter = (char)lastLetterAsInt;

            // Cut off the last letter & replace it with the new letter
            if (s_PlayerName.Length > 1) s_PlayerName = s_PlayerName.Substring(0, s_PlayerName.Length - 1) + newLastLetter;
            else s_PlayerName = newLastLetter.ToString();
        }
    }

    void ScoreboardDecrementLetter()
    {
        if (s_PlayerName.Length == 0) s_PlayerName = "Z";
        else
        {
            // Get the last letter of the player name
            int lastLetterAsInt = s_PlayerName[s_PlayerName.Length - 1];

            if (lastLetterAsInt > 65) lastLetterAsInt -= 1; else lastLetterAsInt = 90;

            char newLastLetter = (char)lastLetterAsInt;

            // Cut off the last letter & replace it with the new letter
            if (s_PlayerName.Length > 1) s_PlayerName = s_PlayerName.Substring(0, s_PlayerName.Length - 1) + newLastLetter;
            else s_PlayerName = newLastLetter.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Determine if the recent score goes on the Scoreboard
        // If we are currently accepting user input...
        if (scoreboardState == ScoreboardState.ReceiveName)
        {
            // Activate the icons
            go_DPad.GetComponent<Image>().enabled = true;
            go_LStick.GetComponent<Image>().enabled = true;
            go_Keyboard.GetComponent<Image>().enabled = true;

            #region Keyboard Input
            // Accept Keyboard Input
            string keyInput = Input.inputString;
            if (s_PlayerName.Length < 5 || s_PlayerName == "") s_PlayerName += keyInput;
            if (s_PlayerName.Length == 0) s_PlayerName = "";

            // if (s_PlayerName.Contains(" ")) s_PlayerName.Replace(" ", "");

            if(Input.GetKeyDown(KeyCode.Backspace) && s_PlayerName.Length > 1)
            {
                s_PlayerName = s_PlayerName.Substring(0, s_PlayerName.Length - 2);

                if (s_PlayerName.Length == 0) s_PlayerName = "";
            }

            // Flash the text on screen
            f_flashTimer += Time.deltaTime;
            if (f_flashTimer >= 0.3f) HighScoreNameObject.GetComponent<Text>().text = "_" + s_PlayerName + "_";
            else HighScoreNameObject.GetComponent<Text>().text = "_" + s_PlayerName + "|_";

            if (f_flashTimer >= 0.6f) f_flashTimer = 0.0f;
            #endregion

            // Record input
            prevState = state;
            state = GamePad.GetState(playerIndex);

            // Accept Keypad Input
            #region Controller Input

            // If the player presses up, we need to take the last letter and 'increase' the character.
            if((state.ThumbSticks.Left.Y >= 0.1f && prevState.ThumbSticks.Left.Y < 0.1f) || (state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released))
            {
                ScoreboardIncrementLetter();
            }
            else if((state.ThumbSticks.Left.Y <= -0.1f && prevState.ThumbSticks.Left.Y > -0.1f) || (state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released))
            {
                ScoreboardDecrementLetter();
            }
            else if(((state.ThumbSticks.Left.X >= 0.1f && prevState.ThumbSticks.Left.X <= 0.1f) && s_PlayerName.Length < 5 ) || 
                (state.DPad.Right == ButtonState.Pressed && prevState.DPad.Right == ButtonState.Released) && s_PlayerName.Length < 5)
            {
                s_PlayerName = s_PlayerName + "A";
            }
            else if ((state.ThumbSticks.Left.X <= -0.1f && prevState.ThumbSticks.Left.X > -0.1f) || (state.DPad.Left == ButtonState.Pressed && prevState.DPad.Left == ButtonState.Released))
            {
                if (s_PlayerName.Length > 0) s_PlayerName = s_PlayerName.Substring(0, s_PlayerName.Length - 1);
            }
            #endregion

            // Submit the name when buttons are pressed
            if (Input.GetKeyDown(KeyCode.Return) || (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A != ButtonState.Pressed))
            {
                // Deactivate the icons
                go_DPad.GetComponent<Image>().enabled = false;
                go_LStick.GetComponent<Image>().enabled = false;
                go_Keyboard.GetComponent<Image>().enabled = false;

                // Store the player's name & score in the Reward system
                ScoreboardInfo scoreInfo = gameObject.GetComponent<Cs_RewardSystem>().GetScoreboardInfo();

                //print("The Score Position: " + i_ScorePos);

                // Shift names down a position
                for (int i = 5; i > i_ScorePos; --i)
                {
                    if (i == 5) scoreInfo.Name_5th = scoreInfo.Name_4th;
                    if (i == 4) scoreInfo.Name_4th = scoreInfo.Name_3rd;
                    if (i == 3) scoreInfo.Name_3rd = scoreInfo.Name_2nd;
                    if (i == 2) scoreInfo.Name_2nd = scoreInfo.Name_1st;
                }

                // Store the new name
                if (i_ScorePos == 5) scoreInfo.Name_5th = s_PlayerName;
                if (i_ScorePos == 4) scoreInfo.Name_4th = s_PlayerName;
                if (i_ScorePos == 3) scoreInfo.Name_3rd = s_PlayerName;
                if (i_ScorePos == 2) scoreInfo.Name_2nd = s_PlayerName;
                if (i_ScorePos == 1) scoreInfo.Name_1st = s_PlayerName;

                // Reload the Scoreboard & display
                gameObject.GetComponent<Cs_RewardSystem>().SetScoreboardInformation(scoreInfo);


                VictoryTextObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);
                HighScoreNameObject.GetComponent<Text>().color = new Color(0, 0, 0, 0);

                // Change state
                StartShowScoreboard();
                scoreboardState = ScoreboardState.Display;
            }

        }
        else if (scoreboardState == ScoreboardState.Display)
        {
            // Otherwise...
            ShowLetters();

            f_ScoreboardTimer += Time.deltaTime;

            if(f_ScoreboardTimer >= 10f)
            {
                GameObject player = GameObject.Find("Player");
                player.GetComponent<Player>().Die();
                player.GetComponent<Player>().Reset();
                player.GetComponentInChildren<Avatar>().Reset();

                gameObject.SetActive(false);
                gameObject.GetComponent<Canvas>().enabled = false;

                MainMenu.SetActive(true);
                MainMenu.GetComponent<Canvas>().enabled = true;
            }
        }
    }
}
