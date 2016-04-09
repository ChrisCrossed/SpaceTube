/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   Avatar.cs

Description:
Deals with interactivity with the player object (does things when things
 happen to the player)

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure;


public class Avatar : MonoBehaviour
{
    public GameObject DeadScreen;
    public Text ScoreLabel;
	public ParticleSystem trail, burst;

    public GameObject ScoreboardScreen;

    public Renderer Body, Booster;
	public Player player;

	public float deathCountdown = 1f;
    public float HP;

    public float currentHP;
    private bool isDead;
    public int fHits;

    public HUD hud;
    public Transform car;
    public ScreenShake Shake;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private void Start()
    {

    }

    private void OnEnable()
    {
        Cursor.visible = false;
        currentHP = HP;
        isDead = false;
        fHits = 0;
        ParticleSystem.EmissionModule emTrail = trail.emission;
        emTrail.enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        deathCountdown = 1;
    }

    private void OnTriggerEnter (Collider collider)
    {
        --currentHP;
        ++fHits;
        burst.Emit(30);
        StartCoroutine(ShipHit());
        StartCoroutine(ShipFlash());
        Shake.Shake();
        hud.OnHit(fHits);

        if (currentHP <= 0 && isDead == false)
        {
            ParticleSystem.EmissionModule emTrail = trail.emission;
            emTrail.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            burst.Emit(burst.maxParticles);
            //deathCountdown = burst.startLifetime;
            isDead = true;
        }

	}
	
	private void Update ()
    {
        //prevState = state;
        //state = GamePad.GetState(playerIndex);

        hud.SetHP(currentHP);
        if (isDead)
        {
			deathCountdown -= Time.deltaTime;
            if (deathCountdown <= 0f && player.bDead == false)
            {
				deathCountdown = 1f;
                ParticleSystem.EmissionModule emTrail = trail.emission;
                emTrail.enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                player.bDead = true;

                // If score is good enough to be on the Scoreboard...
                player.GetComponent<Cs_RewardSystem>().LoadRewardsFromFile();
                int lowestScore = (int)player.GetComponent<Cs_RewardSystem>().GetScoreboardInfo().Score_5th;
                int playerScore = (int)(player.distanceTraveled * 10f);
                
                if (playerScore >= lowestScore)
                {
                    // ... Start up the scoreboard and pass in the score
                    ScoreboardScreen.SetActive(true);
                    ScoreboardScreen.GetComponent<Canvas>().enabled = true;
                    ScoreboardScreen.GetComponent<Cs_ScoreboardLogic>().SetScoreboardState(ScoreboardState.Start);
                    ScoreboardScreen.GetComponent<Cs_ScoreboardLogic>().SetPlayerScore(playerScore);
                }
                else
                {
                    // Otherwise, load the normal death screen
                    DeadScreen.SetActive(true);
                    DeadScreen.GetComponent<Canvas>().enabled = true;
                    ScoreLabel.text = (playerScore).ToString();
                }
                // print(player.GetComponent<Cs_RewardSystem>().GetScoreboardInfo());

                Cursor.visible = true;
                player.Die();          
            }
		}

        //Glen What the Fuck
        if (Input.GetKeyUp(KeyCode.L))
        {
            BoxCollider Jim = GetComponent<BoxCollider>();

            if (Jim.enabled == false)
            {
                //print("on collider " + Jim.enabled);
                Jim.enabled = true;

            }
            else if(Jim.enabled == true)
            {
                //print("off collider " + Jim.enabled);
                Jim.enabled = false;
            }
        }
    }

    public void Reset()
    {
        currentHP = HP;
        isDead = false;
        fHits = 0;
        hud.HudReset();
    }

    IEnumerator ShipHit()
    {
        if(currentHP >= 1)
        {
            GamePad.SetVibration(playerIndex, 0.3f, 0.3f);
            GetComponent<SoundClass>().PlayClassSound();
            car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, -60), 10 * Time.deltaTime);
            yield return new WaitForSeconds(0.25f);
            car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, 60), 10 * Time.deltaTime);
            GamePad.SetVibration(playerIndex, 0, 0);
            // Tell the player they took a hit (Used for Achievements)
            player.GetComponent<Player>().HitObject();
        }

        else
        {
            GamePad.SetVibration(playerIndex, 1f, 1f);
            GetComponent<SoundClass>().PlayClassSound();
            car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, -90), 10 * Time.deltaTime);
            yield return new WaitForSeconds(0.25f);
            car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, 90), 10 * Time.deltaTime);
            GamePad.SetVibration(playerIndex, 0, 0);
            // Tell the player they took a hit (Used for Achievements)
            player.GetComponent<Player>().HitObject();
        }

    }

    IEnumerator ShipFlash()
    {
        
        GetComponent<BoxCollider>().enabled = false;
        Booster.enabled = false;
        Body.enabled = false;
        yield return new WaitForSeconds(0.2f);
        Booster.enabled = true;
        Body.enabled = true;
        yield return new WaitForSeconds(0.2f);
        Booster.enabled = false;
        Body.enabled = false;
        yield return new WaitForSeconds(0.2f);

        if(!isDead)
        {
            GetComponent<BoxCollider>().enabled = true;
            Booster.enabled = true;
            Body.enabled = true;
        }    
    }
}