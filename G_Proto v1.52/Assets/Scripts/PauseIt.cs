using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PauseIt : MonoBehaviour {

    public bool bInGame = false;

    public Canvas cPauseMenu;
    public GameObject gPauseMenu;
    public ChangeStartingButton changeButton;
    public GameObject buttonTarget;

    public MainMenu mmMainMenu;
    public Player pPlayer;

    public GameObject gHowToPlay;
    public Canvas cHowToPlay;

    public GameObject gConfirm;
    public Canvas cConfirm;

    public bool bInHowtoPlay;
    private bool bIsPaused;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Use this for initialization
    void Start ()
    {
        //bInGame = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (Input.GetKeyUp(KeyCode.Escape) && bInGame)
        {
            //print("why is this toomuch to ask for?");
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
            gPauseMenu.SetActive(false);
            cPauseMenu.enabled = false;
        }
        else
        {
            Time.timeScale = 0;
            bIsPaused = true;
            gPauseMenu.SetActive(true);
            cPauseMenu.enabled = true;
            changeButton.ChangeButton(buttonTarget);
        }
    }

    public void MenuReturn()
    {
        mmMainMenu.EndGame(pPlayer.distanceTraveled);
        Time.timeScale = 1;
        bIsPaused = false;
        cPauseMenu.enabled = false;
    }


    public void Resume()
    {
        Time.timeScale = 1;
        bIsPaused = false;
        gPauseMenu.SetActive(false);
        cPauseMenu.enabled = false;
        cHowToPlay.enabled = false;
    }


}
