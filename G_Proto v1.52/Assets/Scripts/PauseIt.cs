using UnityEngine;
using System.Collections;

public class PauseIt : MonoBehaviour {

    public bool bInGame;

    public Canvas cPauseMenu;
    public MainMenu mmMainMenu;
    public Player pPlayer;
    public Canvas cHowToPlay;


    public bool bInHowtoPlay;

    private bool bIsPaused;

	// Use this for initialization
	void Start ()
    {

        bInGame = false;

	}
	
	// Update is called once per frame
	void Update ()
    {
	
        if(Input.GetKeyUp(KeyCode.Escape) && bInGame)
        {
            if(bIsPaused == true)
            {

                Time.timeScale = 1;
                bIsPaused = false;
                cPauseMenu.enabled = false;
                cHowToPlay.enabled = false;
                bInHowtoPlay = false;


            }
            else
            {

                Time.timeScale = 0;
                bIsPaused = true;
                cPauseMenu.enabled = true;
                
            }

        }
        

    }

    public void MenuReturn()
    {

        mmMainMenu.EndGame(pPlayer.distanceTraveled);
        Time.timeScale = 1;
        bIsPaused = false;
        cPauseMenu.enabled = false;

    }

    public void HowToPlay()
    {

        if (bInHowtoPlay == true)
        {

            cHowToPlay.enabled = false;
            cPauseMenu.enabled = true;
            bInHowtoPlay = false;

        }
        else
        {

            cHowToPlay.enabled = true;
            cPauseMenu.enabled = false;
            bInHowtoPlay = true;

        }

    }

    public void Resume()
    {

        Time.timeScale = 1;
        bIsPaused = false;
        cPauseMenu.enabled = false;

    }


}
