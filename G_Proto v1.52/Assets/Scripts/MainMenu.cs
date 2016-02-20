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

    public ChangeStartingButton ButtonChanger;
    public GameObject ButtonTarget;

	private void Awake ()
    {
		Application.targetFrameRate = 1000;    
    }
    void OnEnable()
    {

    }

	public void StartGame (int mode)
    {

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
                
        yield return new WaitForSeconds(1.5f);
        
        player.StartGame(mode);
        player.GetComponent<SoundClass>().SetAudioObject();
        player.GetComponent<SoundClass>().PlayInportedSound(StartNoise, false);
        gameObject.SetActive(false);
        BGMPlayer.GetComponent<SoundClass>().PlayInportedSound(SelectedTrack, true);
        PauseMenu.bInGame = true;
        
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
