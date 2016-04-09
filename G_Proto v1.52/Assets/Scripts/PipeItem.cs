/*******************************  SpaceTube  *********************************
Author: Daya Singh
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   PipeItem.cs

Description:
constructs sigular pipes

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;

public class PipeItem : MonoBehaviour {

	private Transform rotater;

	private void Awake () {
		rotater = transform.GetChild(0);
	}

	public void Position (Pipe pipe, float curveRotation, float ringRotation) {
		transform.SetParent(pipe.transform, false);
		transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
		rotater.localPosition = new Vector3(0f, pipe.CurveRadius);
		rotater.localRotation = Quaternion.Euler(ringRotation, 0f, 0f);
	}
}