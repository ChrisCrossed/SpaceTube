/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   LoadScene.cs

Description:
button logic for loading scenes

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{

	// Use this for initialization
    public void ClickLoad(string Level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Level);
    }
}
