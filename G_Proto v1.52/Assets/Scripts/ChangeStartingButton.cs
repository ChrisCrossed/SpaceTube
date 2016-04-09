/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   ChangeStartButton.cs

Description:
changes the start button

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ChangeStartingButton : MonoBehaviour
{
    public EventSystem eEventSystem;

    public void ChangeButton(GameObject gTarget_)
    {
        eEventSystem.SetSelectedGameObject(gTarget_);
    }
}
