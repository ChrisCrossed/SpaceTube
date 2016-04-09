/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   RandomPlacer.cs

Description:
places random obstacles

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;

public class RandomPlacer : PipeItemGenerator {

	public PipeItem[] itemPrefabs;

	public override void GenerateItems (Pipe pipe) {
		float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
		for (int i = 0; i < pipe.CurveSegmentCount; i++) {
			PipeItem item = Instantiate<PipeItem>(
				itemPrefabs[Random.Range(0, itemPrefabs.Length)]);
			float pipeRotation =
				(Random.Range(0, pipe.pipeSegmentCount) + 0.5f) *
				360f / pipe.pipeSegmentCount;
			item.Position(pipe, i * angleStep, pipeRotation);
		}
	}
}