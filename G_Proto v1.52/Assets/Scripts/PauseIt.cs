using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PauseIt : MonoBehaviour {

    public bool bInGame;

    public Canvas cPauseMenu;
    public MainMenu mmMainMenu;
    public Player pPlayer;
    public Canvas cHowToPlay;
    public ControllerMenu cmMenu;

    public bool bInHowtoPlay;

    private bool bIsPaused;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Use this for initialization
    void Start ()
    {
        bInGame = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (Input.GetKeyUp(KeyCode.Escape) && bInGame)
        {
            Pause();
        }

        if(state.Buttons.Start == ButtonState.Pressed && prevState.Buttons.Start == ButtonState.Released)
        {
            Pause();
        }
    }
    private void Pause()
    {
        if (bIsPaused == true)
        {
            Time.timeScale = 1;
            bIsPaused = false;
            cPauseMenu.enabled = false;
            cHowToPlay.enabled = false;
            bInHowtoPlay = false;
           // cmMenu.enabled = false;
            //cmMenu.ToggleOn();
        }
        else
        {
            Time.timeScale = 0;
            bIsPaused = true;
            cPauseMenu.enabled = true;
            //cmMenu.enabled = true;
            //cmMenu.ToggleOn();
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
