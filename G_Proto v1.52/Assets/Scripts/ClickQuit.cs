/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   ClickQuit.cs

Description:
Quit interactivty for buttons that quit out of game

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class ClickQuit : MonoBehaviour
{
    // for thefucks
    // Use this for initialization
    void Start()
    {
        Cursor.visible = true;
    }

    public void ClickClose()
    {
        Application.Quit();
    }
}

