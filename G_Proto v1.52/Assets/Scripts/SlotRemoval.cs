/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   SlotRemoval.cs

Description:
Places aisles in pipes

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class SlotRemoval : MonoBehaviour
{

	void Start ()
    {
        Transform[] Children = GetComponentsInChildren<Transform>();

        Children[Random.Range(1, 2)].gameObject.SetActive(false);

    }
}
