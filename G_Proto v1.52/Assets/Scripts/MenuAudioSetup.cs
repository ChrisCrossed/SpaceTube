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
