using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

  private bool MuteAllAudio_;
  private bool MuteBGM_;

	// Use this for initialization
	void Start () 
  {
    MuteAllAudio_ = false;
    MuteBGM_ = false;
	}
	
	// Update is called once per frame
	void Update () 
  {
    if (Screen.fullScreen)
    {
      Text textObj = GameObject.Find("FullscreenButton").transform.GetChild(0).transform.GetComponent<Text>();
      textObj.text = "Switch to Windowed";
    }
    else
    {
      Text textObj = GameObject.Find("FullscreenButton").transform.GetChild(0).transform.GetComponent<Text>();
      textObj.text = "Switch to Fullscreen";
    }

    if(MuteAllAudio_)
    {
      Text textObj = GameObject.Find("MuteAllSound").transform.GetChild(0).transform.GetComponent<Text>();
      textObj.text = "Unmute All Sound";
    }
    else
    {
      Text textObj = GameObject.Find("MuteAllSound").transform.GetChild(0).transform.GetComponent<Text>();
      textObj.text = "Mute All Sound";
    }

    if(MuteBGM_)
    {
      Text textObj = GameObject.Find("MuteMusicButton").transform.GetChild(0).transform.GetComponent<Text>();
      textObj.text = "Unmute Music";
    }
    else
    {
      Text textObj = GameObject.Find("MuteMusicButton").transform.GetChild(0).transform.GetComponent<Text>();
      textObj.text = "Mute Music";
    }

    MuteBGM_ = GameObject.Find("Shaker").GetComponent<AudioSource>().mute;
	}


  public void MuteAllAudio()
  {
    MuteAllAudio_ = !MuteAllAudio_;
    AudioSource[] sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    foreach (AudioSource sound in sounds)
    {
      sound.mute = MuteAllAudio_;
    }
    
  }

  public void MuteBGM()
  {
    AudioSource BGM = GameObject.Find("Shaker").GetComponent<AudioSource>();
    BGM.mute = !BGM.mute;
    MuteBGM_ = !MuteBGM_;
  }

  public void SwitchWindow()
  {
    Screen.fullScreen = !Screen.fullScreen;
  }
}
