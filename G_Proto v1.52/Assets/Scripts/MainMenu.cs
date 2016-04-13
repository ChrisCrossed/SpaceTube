/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   MainMenu.cs

Description:
handles Main menu logic

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

	public Player player;

	public Text scoreLabel;

    public GameObject BGMPlayer;

    public AudioClip SelectedTrack;

    public PauseIt PauseMenu;

    public AudioClip StartNoise;
    public AudioClip EndNoise;
    public AudioClip DefaultSong;

    public ChangeStartingButton ButtonChanger;
    public GameObject ButtonTarget;

	private void Awake ()
    {
		Application.targetFrameRate = 1000;
    
    }
    void OnEnable()
    {
      bool initial = PlayerPrefs.HasKey("MuteAllAudio");
      if (initial)
      {
        int check = PlayerPrefs.GetInt("MuteAllAudio");
        if (check == 1)
        {
          AudioSource[] sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
          foreach (AudioSource sound in sounds)
          {
            sound.mute = true;
          }
          AudioSource BGM = GameObject.Find("Main Menu").GetComponent<AudioSource>();
          BGM.mute = true;
        }

        check = PlayerPrefs.GetInt("MuteBGM");

        if (check == 1)
        {
          AudioSource BGM = GameObject.Find("Shaker").GetComponent<AudioSource>();
          BGM.mute = true;
          BGM = GameObject.Find("Main Menu").GetComponent<AudioSource>();
          BGM.mute = true;
        }
        else
        {
          AudioSource BGM = GameObject.Find("Shaker").GetComponent<AudioSource>();
          BGM.mute = false;
          BGM = GameObject.Find("Main Menu").GetComponent<AudioSource>();
          BGM.mute = false;
        }
      }
    }

	public void StartGame (int mode)
    {
        //print("Corutine?");
        StartCoroutine(Begin(mode));

    }

	public void EndGame (float distanceTraveled)
    {
        gameObject.SetActive(true);
        BGMPlayer.GetComponent<SoundClass>().Stop();
        PauseMenu.bInGame = false;
        scoreLabel.text = ((int)(distanceTraveled * 10f)).ToString();
        player.Die();

        //StartCoroutine(End(distanceTraveled));

    }

    public void SelectTrack(AudioClip INasSelectedTrack)
    {

        SelectedTrack = INasSelectedTrack;
        

    }

    public void SelectPipe(Pipe INpSelectedPipe)
    {
        //Pipe timmy = INpSelectedPipe.GetComponent<Pipe>();
        //player.pipeSystem.pipePrefab = timmy;
        player.pipeSystem.pipePrefab = INpSelectedPipe;
    }


    public void DeadWait()
    {

        BGMPlayer.GetComponent<SoundClass>().Stop();
        PauseMenu.bInGame = false;
        
    }

    IEnumerator Begin(int mode)
    {
        //print("begin");        
        yield return new WaitForSeconds(1.5f);
        //print("yielded");
        player.StartGame(mode);
        player.GetComponent<SoundClass>().SetAudioObject();
        player.GetComponent<SoundClass>().PlayInportedSound(StartNoise, false);
        gameObject.SetActive(false);
        BGMPlayer.GetComponent<SoundClass>().PlayInportedSound(SelectedTrack, true);
        if(SelectedTrack == null)
        {
            BGMPlayer.GetComponent<SoundClass>().PlayInportedSound(DefaultSong, true);
        }
        PauseMenu.bInGame = true;
        bool initial = PlayerPrefs.HasKey("MuteAllAudio");
        if (initial)
        {
          int check = PlayerPrefs.GetInt("MuteAllAudio");
          if (check == 1)
          {
            AudioSource[] sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource sound in sounds)
            {
              sound.mute = true;
            }
          }
          check = PlayerPrefs.GetInt("MuteBGM");
          if (check == 1)
          {
            AudioSource BGM = GameObject.Find("Shaker").GetComponent<AudioSource>();
            BGM.mute = true;
          }
        }
    }

    IEnumerator End(float distanceTraveled)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().enabled = false;
        
        player.GetComponent<SoundClass>().SetAudioObject();
        player.GetComponent<SoundClass>().PlayInportedSound(EndNoise, false);
        yield return new WaitForSeconds(1f);
       
        //gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().enabled = true;
        
    }

}
