﻿/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   MultiWindow.cs

Description:
sets up multiple window obstacles

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/
using UnityEngine;
using System.Collections;

public class MultiWindow : PipeItemGenerator
{
    public int HowManyWindows = 4;
    public GameObject[] itemPrefabs;

    public override void GenerateItems(Pipe pipe)
    {
        float start = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f);

        float direction = 0.75f;

        float angleStep = 2.4f;

        for(int j = 0; j <= HowManyWindows; j++)
        {

            for (int i = 0; i < pipe.CurveSegmentCount + (j + 2) ; i++)
            {
                PipeItem item = Instantiate<PipeItem>(
                    itemPrefabs[Random.Range(0, itemPrefabs.Length)].GetComponent<PipeItem>());
                float pipeRotation =
                    (start + i * direction) * 360f / pipe.pipeSegmentCount;
                item.Position(pipe, angleStep, pipeRotation);
            }
            angleStep += 3.4f;
            //start -= 0.5f;
        }  
    }
}
