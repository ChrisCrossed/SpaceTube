using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XInputDotNetPure;

public class CreditsHandler : MonoBehaviour
{

    private float Timer;

    public float SwapTime;

    public Canvas Credits1;
    public Canvas Credits2;
    public Canvas Credits3;
    public Canvas Credits4;

    private int NextCanvas;

    private bool CanTimer;

    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private float Cooldown = 1f;

    // Use this for initialization
    void Start ()
    {
        Timer = 0;
        CanTimer = true;    
	}
	
	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;

        if (Timer >= SwapTime && CanTimer)
        {
            Timer = 0;
            NextCanvas += 1;
            ChangeSlide(NextCanvas);
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (Cooldown <= 0 && prevState.IsConnected)
        {
            state = GamePad.GetState(playerIndex);
            CheckControllers();
        }

        else
        {
            Cooldown -= Time.deltaTime;
        }

    }
    private void CheckControllers()
    {

        if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed)
        {
            ForwardButton();
        }

        else if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed)
        {
            BackwardButton();
        }
    }

    public void ForwardButton()
    {
        
        Timer = 0;
        NextCanvas += 1;
        if(NextCanvas > 3)
        {

            NextCanvas = 3;

        }
        ChangeSlide(NextCanvas);

    }

    public void BackwardButton()
    {

        Timer = 0;
        NextCanvas -= 1;

        if (NextCanvas < 0)
        {

            NextCanvas = 0;

        }
        ChangeSlide(NextCanvas);

    }

    public void ToggleTimer()
    {
        if(CanTimer)
        {

            CanTimer = false;

        }
        else
        {

            CanTimer = true;

        }


    }

    public void ChangeSlide(int INiNextSlide)
    {

        switch (NextCanvas)
        {
            case 0:
                {
                    Credits1.enabled = true;
                    Credits2.enabled = false;
                    break;
                }
            case 1:
                {
                    Credits1.enabled = false;
                    Credits2.enabled = true;
                    break;
                }
            case 2:
                {

                    Credits2.enabled = false;
                    Credits3.enabled = true;
                    break;
                }
            case 3:
                {
                    Credits3.enabled = false;
                    Credits4.enabled = true;
                    break;
                }
            default:
                {
                    
                    break;

                }

        }


    }
}
