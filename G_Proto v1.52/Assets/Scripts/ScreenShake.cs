/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   ScreenShake.cs

Description:
does screenshake

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public float ShakeAmount;
    private bool bShake;

	// Use this for initialization
	void Start ()
    {
        bShake = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (bShake && Time.timeScale != 0)
        {
            transform.localPosition = Random.insideUnitSphere * ShakeAmount;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
	}

    public void Shake()
    {
        StartCoroutine("ScreenShaker");
    }

    IEnumerator ScreenShaker()
    {
        bShake = true;
        yield return new WaitForSeconds(0.25f);
        bShake = false;
    }
}
