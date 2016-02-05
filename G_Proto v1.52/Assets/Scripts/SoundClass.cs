using UnityEngine;
using System.Collections;

public class SoundClass : MonoBehaviour {

    private bool bIsPlaying;
    private bool bRepeat;
    private bool bPause;
    private bool bStop;
    private AudioSource asObjectsAudio;


    public static int ifResumeFrom;


    public AudioClip acSound;



	// Use this for initialization
	void Start ()
    {

        SetAudioObject();
        //print(ifResumeFrom);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
        if(bRepeat == true && asObjectsAudio.isPlaying == false && bPause == false)
        {

            asObjectsAudio.PlayOneShot(acSound);

        }

	}

    public void PlayInportedSound(AudioClip INacAudioToPlay, bool INbRepeat)
    {
        //print(asObjectsAudio);
        bPause = false;
        asObjectsAudio.PlayOneShot(INacAudioToPlay);
       

        if(INbRepeat == true)
        {

            bRepeat = true;
            acSound = INacAudioToPlay;

        }


    }

    public void PlayClassSound()
    {

        if (acSound != null)
        {
            bPause = false;
            asObjectsAudio.PlayOneShot(acSound);


        }
        else
        {

            print("No Sound");

        }
    }

    public void SetRepeat(bool INbSetRepeat)
    {

        bRepeat = INbSetRepeat;

    }

    public bool GetRepeat()
    { 
        return bRepeat;

    }


    public void SetAudioPause(bool INbSetPause)
    {

        bPause = INbSetPause;
        if(INbSetPause == true)
        {

            asObjectsAudio.Pause();

        }

        if (INbSetPause == false)
        {

            asObjectsAudio.UnPause();

        }


    }

    public bool GetAudioPause()
    {
        return bPause;

    }

    public void SetResumeLocation(int INfResumeLocation)
    {

        ifResumeFrom = INfResumeLocation;

    }

    public void PlayResumedSound(AudioClip INacAudioToResume, bool INbRepeat)
    {

        //print(INacAudioToResume);
        
        asObjectsAudio.PlayOneShot(INacAudioToResume);
        int Timmy = asObjectsAudio.timeSamples + ifResumeFrom;
        asObjectsAudio.timeSamples = Timmy;
        print("Post Time " + asObjectsAudio.timeSamples);

    }

    public void Stop()
    {
        
        asObjectsAudio.Stop();
        bPause = true;

    }

    public void SetAudioObject()
    {
        if(asObjectsAudio == null)
        {

            asObjectsAudio = GetComponent<AudioSource>();

        }
        

    }

}
