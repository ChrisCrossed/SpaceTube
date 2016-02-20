using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public Image iSplashScreen;
    public string sLevelToLoad;
    private bool bFading = false;
    private bool bMenuOn = false;
    private float fScale;

    public Image iNewImage1;
    public Image iNewImage2;
    public float FadeTimer;
    private float Timer;
    private int ImageSpot;

    public GameObject scAudio;
    public GameObject Camera;
    public GameObject MainMenu;
    public Canvas cMainMenu;
    public ChangeStartingButton ChangeButton;
    public GameObject ButtonTarget;

    private void OnEnable()
    {
        bMenuOn = false;
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= FadeTimer && bFading == false)
        {
            //print("Timer to Fade");
            bFading = true;
        }

        if (bFading)
        {
            //print("Fading");
            iSplashScreen.color = Color.Lerp(Color.white, Color.clear, fScale += 0.003f);
            iNewImage1.color = Color.Lerp(Color.white, Color.clear, fScale += 0.003f);
            iNewImage2.color = Color.Lerp(Color.white, Color.clear, fScale += 0.003f);
            //print(Time.time);

            if (iSplashScreen.color == Color.clear && ImageSpot == 0)
            {
                //print("bob");
                bFading = false;
                iNewImage1.color = Color.white;
                Timer = 0;
                fScale = 0;
                iSplashScreen.enabled = false;
                iNewImage1.enabled = true;
                ImageSpot += 1;

            }

            else if (iNewImage1.color == Color.clear && ImageSpot == 1)
            {
                //print("bob");
                bFading = false;
                iNewImage2.color = Color.white;
                Timer = 0;
                fScale = 0;
                iNewImage1.enabled = false;
                iNewImage2.enabled = true;
                ImageSpot += 1;
                //iSplashScreen.color = Color.white;
            }

            else if (iNewImage2.color == Color.clear && ImageSpot == 2)
            {
                if(!bMenuOn)
                {
                    Camera.GetComponent<Canvas>().enabled = false;
                    cMainMenu.enabled = true;
                    MainMenu.SetActive(true);
                    ChangeButton.ChangeButton(ButtonTarget);
                    bMenuOn = true;
                }
            }
        }
        else
        {
            iSplashScreen.color = Color.white;
        }

        if (Input.anyKeyDown)
        {
            //scAudio.GetComponent<SoundClass>().SetResumeLocation(scAudio.GetComponent<AudioSource>().timeSamples);
            if (!bMenuOn)
            {
                Camera.GetComponent<Canvas>().enabled = false;
                cMainMenu.enabled = true;
                MainMenu.SetActive(true);
                ChangeButton.ChangeButton(ButtonTarget);
                bMenuOn = true;
            }
        }
    }

    //Wtf does this do??
    IEnumerator MenuCall()
    {
        bFading = true;
        yield return new WaitForSeconds(6);
        Application.LoadLevel(sLevelToLoad);
        cMainMenu.enabled = true;
        MainMenu.SetActive(true);
        ChangeButton.ChangeButton(ButtonTarget);
        yield break;
    }
}
