using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

  private bool MuteAllAudio_;
  private bool MuteBGM_;

	// Use this for initialization
	void Start () 
  {

    bool initial = PlayerPrefs.HasKey("MuteAllAudio");
    if(initial)
    {
      int check = PlayerPrefs.GetInt("MuteAllAudio");
      if(check == 0)
      {
        MuteAllAudio_ = false;
      }
      else
      {
        MuteAllAudio_ = true;
        AudioSource[] sounds = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource sound in sounds)
        {
          sound.mute = MuteAllAudio_;
        }
      }

      check = PlayerPrefs.GetInt("MuteBGM");
      if (check == 0)
      {
        MuteBGM_ = false;
      }
      else
      {
        MuteBGM_ = true;
      }

    }
    else
    {
      PlayerPrefs.SetInt("MuteAllAudio", 0);
      PlayerPrefs.SetInt("MuteBGM", 0);
    }
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

    if (MuteAllAudio_)
    {
      PlayerPrefs.SetInt("MuteAllAudio", 1);
      PlayerPrefs.SetInt("MuteBGM", 1);
    }
    else
    {
      PlayerPrefs.SetInt("MuteAllAudio", 0);
      PlayerPrefs.SetInt("MuteBGM", 0);
    }
  }

  public void MuteBGM()
  {
    AudioSource BGM = GameObject.Find("Shaker").GetComponent<AudioSource>();
    BGM.mute = !BGM.mute;
    MuteBGM_ = !MuteBGM_;
    if (MuteBGM_)
      PlayerPrefs.SetInt("MuteBGM", 1);
    else
      PlayerPrefs.SetInt("MuteBGM", 0);
  }

  public void SwitchWindow()
  {
    Screen.fullScreen = !Screen.fullScreen;
  }
}
