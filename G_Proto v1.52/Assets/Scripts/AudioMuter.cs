using UnityEngine;
using System.Collections;

public class AudioMuter : MonoBehaviour {

	// Use this for initialization
  void OnEnable()
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
    else
    {
    }

    if(gameObject.name == "Dead")
    {
      check = PlayerPrefs.GetInt("MuteBGM");
      if (check == 1)
      {
        AudioSource muse = GameObject.Find("Dead").GetComponent<AudioSource>();
        muse.mute = true;
      }
      else
      {
        AudioSource muse = GameObject.Find("Dead").GetComponent<AudioSource>();
        muse.mute = false;
      }
    }
  }
}
