using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public class Avatar : MonoBehaviour
{

	public ParticleSystem trail, burst;

    public Renderer Body, Booster;
	public Player player;

	public float deathCountdown = -1f;
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
        currentHP = HP;
        isDead = false;
        fHits = 0;
        ParticleSystem.EmissionModule emTrail = trail.emission;
        emTrail.enabled = true;
        GetComponent<BoxCollider>().enabled = true;
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
            deathCountdown = burst.startLifetime;
            isDead = true;
        }

	}
	
	private void Update ()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        hud.SetHP(currentHP);
        if (isDead)
        {
			deathCountdown -= Time.deltaTime;
			if (deathCountdown <= 0f)
            {
				deathCountdown = -1f;

                ParticleSystem.EmissionModule emTrail = trail.emission;
                emTrail.enabled = false;
                GetComponent<BoxCollider>().enabled = false;
                currentHP = HP;
                isDead = false;
                fHits = 0;
                hud.HudReset();
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

    IEnumerator ShipHit()
    {
        GamePad.SetVibration(playerIndex, 0.5f, 0.5f);
        GetComponent<SoundClass>().PlayClassSound();
        car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, -60), 10 * Time.deltaTime);
        yield return new WaitForSeconds(0.25f);
        car.transform.localRotation = Quaternion.Lerp(car.transform.localRotation, Quaternion.Euler(0, 270, 60), 10 * Time.deltaTime);
        GamePad.SetVibration(playerIndex, 0, 0);
        // Tell the player they took a hit (Used for Achievements)
        player.GetComponent<Player>().HitObject();
    }

    IEnumerator ShipFlash()
    {
        
        GetComponent<BoxCollider>().enabled = false;
        Booster.enabled = false;
        Body.enabled = false;
        yield return new WaitForSeconds(0.25f);
        Booster.enabled = true;
        Body.enabled = true;
        yield return new WaitForSeconds(0.25f);
        Booster.enabled = false;
        Body.enabled = false;
        yield return new WaitForSeconds(0.25f);

        if(!isDead)
        {
            GetComponent<BoxCollider>().enabled = true;
            Booster.enabled = true;
            Body.enabled = true;
        }    
    }
}