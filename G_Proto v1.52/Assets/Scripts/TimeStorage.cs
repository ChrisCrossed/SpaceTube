using UnityEngine;
using System.Collections;

public class TimeStorage : MonoBehaviour 
{
	public float TimeOne;
	public float TimeTwo;
	public float TimeThree;

	private GUIStyle style;

	public string DisplayTime1;
	public string DisplayTime2;
	public string DisplayTime3;
	// Use this for initialization
	void Start () 
	{
		style = new GUIStyle();
		style.fontSize = 16;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
		GUI.Label (new Rect (Screen.width-100, 10, 250, 100), DisplayTime1, style);
		GUI.Label (new Rect (Screen.width-100, 30, 250, 100), DisplayTime2, style);
		GUI.Label (new Rect (Screen.width-100, 50, 250, 100), DisplayTime3, style);
	}
}
