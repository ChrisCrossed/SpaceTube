/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   PipeItemGenerator.cs

Description:
an object to generate pipes

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;

public abstract class PipeItemGenerator : MonoBehaviour {

	public abstract void GenerateItems (Pipe pipe);
}