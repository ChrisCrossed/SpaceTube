/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   PauseIt.cs

Description:
pauses the game when you tell it to

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

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
    private bool audioPaused;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Use this for initialization
    void Start ()
    {
        //bInGame = false;
      audioPaused = false;
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
            Cursor.visible = false;
            Time.timeScale = 1;
            bIsPaused = false;
            gPauseMenu.SetActive(false);
            cPauseMenu.enabled = false;
            if(audioPaused == true)
            {
              AudioSource[] sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
              foreach (AudioSource sound in sounds)
              {
                //sound.Play();
                sound.mute = false;
              }
              audioPaused = false;
            }
        }
        else
        {
            GamePad.SetVibration(playerIndex, 0, 0);
            Cursor.visible = true;
            Time.timeScale = 0;
            bIsPaused = true;
            gPauseMenu.SetActive(true);
            cPauseMenu.enabled = true;
            changeButton.ChangeButton(buttonTarget);
            AudioSource[] sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            if (audioPaused == false)
            {
              foreach (AudioSource sound in sounds)
              {
                //sound.Stop();
                sound.mute = true;
              }
              audioPaused = true;
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


    public void Resume()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        bIsPaused = false;
        gPauseMenu.SetActive(false);
        cPauseMenu.enabled = false;
        cHowToPlay.enabled = false;
        
    }


}
