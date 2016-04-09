/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   PipeItemSpinner.cs

Description:
spins individual pipe item

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class PipeItemSpinner : MonoBehaviour {
    public float SpinSpeed = 0;
    private Transform HostObject;
	// Use this for initialization
	void Start ()
    {

        HostObject = gameObject.transform;

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //print("sgs");
        //HostObject.rotation.SetEulerAngles(HostObject.rotation.x + SpinSpeed, HostObject.rotation.y, HostObject.rotation.z);
        HostObject.Rotate(HostObject.localRotation.x + SpinSpeed, 0, 0);
	}
}
