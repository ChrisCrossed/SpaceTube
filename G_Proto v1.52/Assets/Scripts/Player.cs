﻿/*******************************  SpaceTube  *********************************
Author: Daya Singh
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   Player.cs

Description:
Handles things to do with the player

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool bDead = false;
    public bool bAutoPlay = false;

	public PipeSystem pipeSystem;

	public float startVelocity;
	public float rotationVelocity;

	public MainMenu mainMenu;
	public HUD hud;

	public float[] accelerations;

	private Pipe currentPipe;

	private float acceleration, velocity;
	public float distanceTraveled;
    float distanceTraveled_OneLife;
	private float deltaToRotation;
	private float systemRotation;
	private float worldRotation, avatarRotation;
    private float rotationInput;

    private bool CheckPointHit;
    private float CheckPointTimer;

    public ParticleSystem pboostRing, pFastTrail;
    private int iParticleAmount = 10;

    private float fDeadTimer;
    
    private Transform world, rotater;
    public Transform car;

    public MeshRenderer mrCarRender;
    public MeshRenderer mrBooster;
    public BoxCollider mrCollider;

    //public AudioClip DeadNoise;

    public PlayerIndex playerIndex;
    //GamePadState state;
    //GamePadState prevState;

    // Camera Rotation information
    public GameObject playerCam;
    bool b_CamLockedToPlayerRot;
    //int accelerationMode;

    public void StartGame(int accelerationMode_)
    {
        //print("start game");
        distanceTraveled = 0f;
		avatarRotation = 0f;
		systemRotation = 0f;
		worldRotation = 0f;
        rotationInput = 0f;
		acceleration = accelerations[accelerationMode_];
		velocity = startVelocity;
        pipeSystem.StartGame();
		currentPipe = pipeSystem.SetupFirstPipe();
		SetupCurrentPipe();
		gameObject.SetActive(true);
		hud.SetValues(distanceTraveled, velocity);
        mrBooster.enabled = true;
        mrCarRender.enabled = true;
        mrCollider.enabled = true;
        iParticleAmount = 10;

        // Store acceleration mode
        //accelerationMode = accelerationMode_;

        Vector3 camRot = playerCam.transform.eulerAngles;
        camRot.z = 0f;
        playerCam.transform.eulerAngles = camRot;

        if(!b_CamLockedToPlayerRot)
        {
            ToggleCameraLock();
        }
    }

	public void Die ()
    {
        mrBooster.enabled = false;
        mrCarRender.enabled = false;
        mrCollider.enabled = false;
        mainMenu.BGMPlayer.GetComponent<SoundClass>().Stop();
        // Pass along this live's score to the RewardSystem
        // gameObject.GetComponent<Cs_RewardSystem>().SetRewardInformation(AchievementTypes.HighScore, (int)(distanceTraveled * 10));

        // Convert over to Scoreboard instead
    }

	private void Awake () {
		world = pipeSystem.transform.parent;
		rotater = transform.GetChild(0);
		gameObject.SetActive(false);
        CheckPointHit = true;
        CheckPointTimer = 0;
	}

    public void HitObject()
    {
        // Pass to the RewardSystem that they took a hit
        // gameObject.GetComponent<Cs_RewardSystem>().SetRewardInformation(AchievementTypes.LongestLife, (int)(distanceTraveled_OneLife * 10));
        distanceTraveled_OneLife = 0f;
    }

    public void ToggleCameraLock()
    {
        b_CamLockedToPlayerRot = !b_CamLockedToPlayerRot;

        string tempStr;

        if(b_CamLockedToPlayerRot) tempStr = "Camera: Locked To Ship";
        else tempStr = "Camera: Unlocked";

        if(GameObject.Find("LockCameraButton"))
        {
            GameObject.Find("LockCameraButton").GetComponentInChildren<Text>().text = tempStr;
        }
    }

    private void LateUpdate()
    {
        Vector3 camRot = playerCam.transform.eulerAngles;

        if(b_CamLockedToPlayerRot)
        {
            // Rotate the camera to match the player
            camRot.z = Mathf.LerpAngle(camRot.z, avatarRotation, 0.035f);
        }
        else
        {
            // Bring the camera back to 0 degrees
            camRot.z = Mathf.LerpAngle(camRot.z, 0, 0.035f);
        }

        playerCam.transform.eulerAngles = camRot;
    }

	private void Update ()
    {
        //prevState = state;
        //state = GamePad.GetState(playerIndex);

        velocity += acceleration * Time.deltaTime;
		float delta = velocity * Time.deltaTime;

        // CHEAT CODE: Give player 100 points instantly
        if(Input.GetKeyDown(KeyCode.P))
        {
            distanceTraveled += 10;
            // print("Cheater!!!");
        }

        // Store the current distance travelled.
        if(!bDead)
        {
            distanceTraveled += delta;
            distanceTraveled_OneLife += delta;
        }

		systemRotation += delta * deltaToRotation;

        if(CheckPointHit == true)
        {
            if(CheckPointTimer >= 2)
            {
                CheckPointTimer = 0;
                CheckPointHit = false;
            }
            else
            {
                CheckPointTimer += Time.deltaTime;
            }
        }

		if (systemRotation >= currentPipe.CurveAngle)
        {
			delta = (systemRotation - currentPipe.CurveAngle) / deltaToRotation;
			currentPipe = pipeSystem.SetupNextPipe();
			SetupCurrentPipe();
			systemRotation = delta * deltaToRotation;
		}

		pipeSystem.transform.localRotation =
			Quaternion.Euler(0f, 0f, systemRotation);

		UpdateAvatarRotation();

		hud.SetValues(distanceTraveled, velocity);

        if(Mathf.FloorToInt(distanceTraveled) % 50 == 1)
        {
            if(!CheckPointHit)
            {
                CheckPointHit = true;
                hud.CheckPoint();
                BoostRing();
            }                      
        }             
	}

	private void UpdateAvatarRotation ()
    {
		rotationInput = Input.GetAxis("Horizontal");

        if(bAutoPlay)
        {
            rotationInput = Random.value;
        }

        CarRotation();


		avatarRotation += rotationVelocity * Time.deltaTime * rotationInput;
		if (avatarRotation < 0f)
        {
			avatarRotation += 360f;
            pFastTrail.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

		else if (avatarRotation >= 360f)
        {
			avatarRotation -= 360f;
            pFastTrail.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetButton("Jump"))
        {
            ParticleSystem.EmissionModule emTrail = pFastTrail.emission;
            emTrail.enabled = true;
            avatarRotation += rotationVelocity * Time.deltaTime * rotationInput;
        }
        else
        {
            ParticleSystem.EmissionModule emTrail = pFastTrail.emission;
            emTrail.enabled = false;
        }

        rotater.localRotation = Quaternion.Euler(avatarRotation, 0f, 0f);

	}

	private void SetupCurrentPipe () {
		deltaToRotation = 360f / (2f * Mathf.PI * currentPipe.CurveRadius);
		worldRotation += currentPipe.RelativeRotation;
		if (worldRotation < 0f) {
			worldRotation += 360f;
		}
		else if (worldRotation >= 360f) {
			worldRotation -= 360f;
		}
		world.localRotation = Quaternion.Euler(worldRotation, 0f, 0f);
	}

    private void CarRotation()
    {
        if (rotationInput == 1)
        {
            car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, 30), 10 * Time.deltaTime);
        }
        else if(rotationInput == 0)
        {
            car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, 0), 10 * Time.deltaTime);
        }
        else if (rotationInput == -1)
        {
            car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, -30), 10 * Time.deltaTime);
        }
    }

    private void BoostRing()
    {
        if (iParticleAmount >= pboostRing.maxParticles)
        {
            pboostRing.Emit(pboostRing.maxParticles);
            StartCoroutine(TimedVibration());
        }
        else
        {
            StartCoroutine(TimedVibration());
            pboostRing.Emit(iParticleAmount);
            iParticleAmount += 10;
        }
    }
    public void Reset()
    {
        pipeSystem.KillAllPipes();
        gameObject.SetActive(false);
        bDead = false;
        mainMenu.EndGame(distanceTraveled);
    }

    void OnTriggerEnter(Collider coll)
    {
        Destroy(coll.gameObject);
    }

    IEnumerator TimedVibration()
    {
        GamePad.SetVibration(playerIndex, 1f, 1f);
        yield return new WaitForSeconds(0.25f);
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }

}