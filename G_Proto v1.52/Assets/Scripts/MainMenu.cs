using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

	public Player player;

	public Text scoreLabel;

    public GameObject BGMPlayer;

    public AudioClip SelectedTrack;

    public GameObject PauseMenu;

    public AudioClip StartNoise;
    public AudioClip EndNoise;

	private void Awake () {
		Application.targetFrameRate = 1000;

        
    }

	public void StartGame (int mode)
    {

        StartCoroutine(Begin(mode));

    }

	public void EndGame (float distanceTraveled)
    {
        gameObject.SetActive(true);
        BGMPlayer.GetComponent<SoundClass>().Stop();
        PauseMenu.GetComponent<PauseIt>().bInGame = false;
        scoreLabel.text = ((int)(distanceTraveled * 10f)).ToString();

        print("Travelled: " + distanceTraveled);
        // CHRIS - Receive the distanceTravelled score and apply an overlay if it's greater than any score from the Rewards script
        ScoreboardInfo scoreboardInfo = gameObject.GetComponent<Cs_RewardSystem>().GetScoreboardInfo();
        print(scoreboardInfo.Score_5th);

        // Check the new distanceTravelled against each of the five scores.
        int newScorePos = CheckIfNewHighScore(scoreboardInfo, distanceTraveled);

        // Shift the lower scorer names down & place the new one in
        if (newScorePos > 0) // If it is greater than any score... 
        {
            print("New Score Pos: " + newScorePos);

            // Shift the lower scores down & place the new one in.
            for(var i = 5; i >= newScorePos; --i)
            {
                // If i 
                if (i == newScorePos)
                {
                    print("New high score at position " + i);

                    // Replace the current slots with the score earned
                    if (i == 5) scoreboardInfo.Score_5th = (uint)(distanceTraveled * 10);
                    if (i == 4) scoreboardInfo.Score_4th = (uint)(distanceTraveled * 10);
                    if (i == 3) scoreboardInfo.Score_3rd = (uint)(distanceTraveled * 10);
                    if (i == 2) scoreboardInfo.Score_2nd = (uint)(distanceTraveled * 10);
                    if (i == 1) scoreboardInfo.Score_1st = (uint)(distanceTraveled * 10);

                    print(scoreboardInfo.Score_5th);
                }
                else // Normal case.
                {
                    print("Replacing positions " + i + " & " + (i - 1));
                    // Start at the bottom rung
                    if (i == 5) scoreboardInfo.Score_5th = scoreboardInfo.Score_4th;
                    if (i == 4) scoreboardInfo.Score_4th = scoreboardInfo.Score_3rd;
                    if (i == 3) scoreboardInfo.Score_3rd = scoreboardInfo.Score_2nd;
                    if (i == 2) scoreboardInfo.Score_2nd = scoreboardInfo.Score_1st;
                    // If i == 1, they have the new best score and has been moved down.
                }
            }
            // Record the player's name
            
        }

        // Save the scoreboard
        gameObject.GetComponent<Cs_RewardSystem>().SetScoreboardInformation(scoreboardInfo);

        // Now show the new Scoreboard
        // GameObject.Find("Scoreboard").gameObject.SetActive(true);
    }

    int CheckIfNewHighScore(ScoreboardInfo scoreboardInfo_, float distanceTravelled_)
    {
        int returnPosition = 0;
        
        // If greater than 5th place
        if (distanceTravelled_ * 10 > scoreboardInfo_.Score_5th) returnPosition = 5;

        // If greater than 4th place
        if (distanceTravelled_ * 10 > scoreboardInfo_.Score_4th) returnPosition = 4;

        // If greater than 3th place
        if (distanceTravelled_ * 10 > scoreboardInfo_.Score_3rd) returnPosition = 3;

        // If greater than 2th place
        if (distanceTravelled_ * 10 > scoreboardInfo_.Score_2nd) returnPosition = 2;

        // If greater than 1th place
        if (distanceTravelled_ * 10 > scoreboardInfo_.Score_1st) returnPosition = 1;
        
        print("Position returned: " + returnPosition);

        return returnPosition;
    }

    public void SelectTrack(AudioClip INasSelectedTrack)
    {

        SelectedTrack = INasSelectedTrack;

    }

    public void DeadWait()
    {

        BGMPlayer.GetComponent<SoundClass>().Stop();
        PauseMenu.GetComponent<PauseIt>().bInGame = false;
        
    }

    IEnumerator Begin(int mode)
    {
                
        yield return new WaitForSeconds(1.5f);
        
        player.StartGame(mode);
        player.GetComponent<SoundClass>().SetAudioObject();
        player.GetComponent<SoundClass>().PlayInportedSound(StartNoise, false);
        gameObject.SetActive(false);
        BGMPlayer.GetComponent<SoundClass>().PlayInportedSound(SelectedTrack, true);
        PauseMenu.GetComponent<PauseIt>().bInGame = true;
        
    }

    #region Not used
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
    #endregion

}
