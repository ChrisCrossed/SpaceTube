﻿/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   ControllerMenu.cs

Description:
COntroller use with menus

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XInputDotNetPure;

public class ControllerMenu : MonoBehaviour
{
    public GameObject[] Options;
    private int Size;

    private int Location = 0;

    private float Cooldown = 0.2f;

    public bool bEnabled = false;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Use this for initialization
    void Start()
    {
        Size = Options.Length;
        Location = 0;
    }

    // Update is called once per frame
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);
        if (bEnabled)
        {
            if (Cooldown <= 0)
            {
                if (state.IsConnected)
                {
                    //print(state.IsConnected);
                    // Get the state //
                    state = GamePad.GetState(playerIndex);

                    // Check Player 1's controller for input //
                    CheckControllers();
                }
            }

            else
            {
                Cooldown -= 0.1f;
            }
            CheckControllers();

        }
    }
    private void CheckControllers()
    {
        // If the player pressed A then tell the button to perform its action //
        if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
        {
            Options[Location].GetComponent<Button>().onClick.Invoke();
        }

        float vertical = state.ThumbSticks.Left.Y;

        // Not enough Vertical input //
        if (vertical >= -0.5 && vertical <= 0.5)
        {
            vertical = 0;
        }

        // Move Up in the menu //
        if (vertical >= 0.5)
        {
            Location -= 1;
            if (Location == -1)
            {
                Location = Size - 1;
            }
            Cooldown = 0.2f;
        }

        // Move down in the menu //
        if (vertical <= -0.5)
        {
            Location += 1;
            if (Location == Size)
            {
                Location = 0;
            }
            Cooldown = 0.2f;
        }
        //the the locations moves, highight the button
        HighlightButtons();
    }

  private void HighlightButtons()
    {
        for (int i = 0; i < Options.Length; i++)
        {
            //don't highlight anything but the location
            if(i != Location)
            {
                Options[i].GetComponent<Image>().color = Color.white;
            }

            else
            {
                Options[Location].GetComponent<Image>().color = Color.cyan;
            }
        }     
    }

public void ToggleOn()
    {
        if(bEnabled)
        {
            bEnabled = false;
        }

        else
        {
            bEnabled = true;
        }
    }

}
