/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   WallDetection.cs

Description:
Detects obstructions to camera and deals with them

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class WallDetection : MonoBehaviour
{
    public GameObject Target;
    private Vector3 Direction;
    public bool DebugDraw;
    private Transform Obstruction;
	// Use this for initialization
	void Start ()
    {
        Obstruction = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Direction = Target.transform.position - transform.position;
        RaycastHit HitInfo;
        Physics.Raycast(transform.position, Direction, out HitInfo);
        
        if(HitInfo.transform.name != Target.name && HitInfo.transform.GetComponent<Renderer>())
        {
            Obstruction = HitInfo.transform;
            Color color = HitInfo.transform.GetComponent<Renderer>().material.color;
            color.a = 0.5f;
            HitInfo.transform.GetComponent<Renderer>().material.color = color;
        }
        else if(HitInfo.transform.name == Target.name && Obstruction)
        {
            Obstruction.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }

        if(DebugDraw)
        {
            Debug.DrawRay(transform.position, Direction);
        }
    }
}
