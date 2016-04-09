/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   Timer.cs

Description:
does timer logic

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
	public  float timer;
	private int minutes;
	private int seconds;
	private int miliseconds;
	private GUIStyle style;
	public string DisplayTime;
	
	// Use this for initialization
	void Start () 
	{ 
		style = new GUIStyle();
		style.fontSize = 24;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer = Time.time;

		minutes = Mathf.FloorToInt(timer / 60F);
		seconds = Mathf.FloorToInt(timer);

		if (miliseconds > 999) 
		{
			miliseconds = 0;
		}

		else
		{
			miliseconds = Mathf.FloorToInt (timer * 1000F);
		}
	}

	void OnGUI()
	{
		DisplayTime = string.Format ("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
		GUI.Label(new Rect(10,10,250,100), DisplayTime, style);
	}


	
}
