using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    // Use this for initialization
    void Start()
    {
        // StartShowScoreboard();
    }

    public void SetPlayerScore(int playerScore_)
    {
        // We already determined that the player's score is greater than 5th place
        // Determine what position the score is

        // Ask for the player's name

        // Store the player's name & score in the Reward system

        // Reload the Scoreboard & display

    }

    public void StartShowScoreboard()
    {
        // Load GameObjects
        LoadGameObjects();

        // Load names & scores
        LoadInformation();

        // Reset timers


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
                print(ScoreGroup[currScore_].GetComponent<Text>().text);
            }

            ++ScoreLength_Curr[currScore_];
        }
    }

    void ShowLetters()
    {
        // Increment timer for next allowed word
        if (i_ScoreCounter < 4) f_Timer += Time.deltaTime;
        f_LetterTimer += Time.deltaTime;

        print("Length: " + NameLength_Curr[0]);
        print("Timer: " + f_LetterTimer);

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
    }

    // Update is called once per frame
    void Update()
    {
        // Determine if the recent score goes on the Scoreboard

        // If it doesn't...

        // Otherwise...
        ShowLetters();
    }
}
