/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   MenuAudioSetup.cs

Description:
Setting up menu audio 

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/
using UnityEngine;
using System.Collections;

public class MenuAudioSetup : MonoBehaviour {

    public AudioClip ResumedTrack;
    private SoundClass ItsAThing;

	// Use this for initialization
	void Start ()
    {

        ItsAThing = GetComponent<SoundClass>();
        //print(ResumedTrack);
        
        ItsAThing.SetAudioObject();
        
        GetComponent<SoundClass>().PlayResumedSound(ResumedTrack, true);

        //GetComponent<SoundClass>().PlayClassSound();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
