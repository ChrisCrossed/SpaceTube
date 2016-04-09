/*******************************  SpaceTube  *********************************
Author: 
Contributors:
Course: GAM350
Game:   G_Proto
Date:   4/8/16
File:   HUD.cs

Description:
Handles heads up display

Current Problems:


Copyright (C) 2016 DigiPen Institute of Technology.
Reproduction or disclosure of this file or its contents without the prior
written consent of DigiPen Institute of Technology is prohibited.
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using XInputDotNetPure;

public class HUD : MonoBehaviour
{

	public Text distanceLabel, velocityLabel, HPLabel, ButtonText, StickText;
    public Image Flash, Health1, Health2, Health3, AButton, LeftStick, iKeyboard;
    private bool Flashing = false;
    public Transform tBurst;
    public GameObject Avatar;

    public PlayerIndex playerIndex;
    private GamePadState state;

    public AudioClip CheckPointNoise;

    public void SetValues (float distanceTraveled, float velocity)
    {
		distanceLabel.text = ((int)(distanceTraveled * 10f)).ToString();

        if((distanceTraveled%500) == 0)
        {
            Instantiate(tBurst, distanceLabel.transform.position, (new Quaternion(0, 0, 0, 0)));
        }
		//velocityLabel.text = ((int)(velocity * 50f)).ToString();
	}

    private void Start()
    {
        
    }

    public void Tutorial()
    {

        state = GamePad.GetState(playerIndex);
        

        if (state.IsConnected)
        {
            StartCoroutine(GamePadTutorial());
        }

        else
        {
            StartCoroutine(KeyboardTutorial());
        }

    }

    public void Update()
    {
        if(Flashing)
        {
            Flash.color = Color.Lerp(new Vector4(1,0,0,0.5f), Color.clear, 0.25f);
        }
        else
        {
            Flash.color = Color.clear;
        }
    }

    public void SetHP(float hpValue)
    {
        HPLabel.text = ((int)(hpValue * 100f)).ToString();
    }
    public void HudReset()
    {
        Health1.color = Color.green;
        Health2.color = Color.yellow;
        Health3.color = Color.red;
    }

    public void OnHit(int fHits_)
    {
        StartCoroutine("Flasher");
        switch(fHits_)
        {
            case 3:
                {
                    Health3.color = Color.clear;
                    break;
                }
            case 2:
                {
                    Health2.color = Color.clear;
                    break;
                }
            case 1:
                {
                    Health1.color = Color.clear;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void CheckPoint()
    {

        GetComponent<SoundClass>().PlayInportedSound(CheckPointNoise, false);
        

        switch(Avatar.GetComponent<Avatar>().fHits)
        {

            case 1:
                {
                    Avatar.GetComponent<Avatar>().fHits -= 1;
                    Avatar.GetComponent<Avatar>().currentHP += 1;
                    Flasher();
                    Health1.color = Color.green;
                    break;
                }
            case 2:
                {
                    Avatar.GetComponent<Avatar>().fHits -= 1;
                    Avatar.GetComponent<Avatar>().currentHP += 1;
                    Flasher();
                    Health2.color = Color.yellow;
                    break;
                }
            
            default:
                {
                    break;
                }


        }

    }

    IEnumerator Flasher()
    {
        Flashing = true;
        yield return new WaitForSeconds(0.25f);
        Flashing = false;
    }

    IEnumerator GamePadTutorial()
    {
        
        AButton.color = Color.white;
        LeftStick.color = Color.white;
        StickText.color = Color.white;
        ButtonText.color = Color.white;
        yield return new WaitForSeconds(6f);
        AButton.color = Color.clear;
        LeftStick.color = Color.clear;
        StickText.color = Color.clear;
        ButtonText.color = Color.clear;
    }

    IEnumerator KeyboardTutorial()
    {
        
        iKeyboard.color = Color.white;
        yield return new WaitForSeconds(7f);
        iKeyboard.color = Color.clear;
    }
}