using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization


    [HideInInspector]

    public bool grounded, impulsed;

    [Header("AudioSources")]

    public AudioSource jumpSound;
    public AudioSource explosionSound;
    public AudioSource diyingSound;

    [Header("Animations")]

    public Animation BeatAnimation;
    private bool manageDownBeat,JumpedOnDownBeat,JumpedWhenGrounded;

    [Header("Player Particles")]

    public ParticleSystem myStream;
    public GameObject DeadParticles;

    [HideInInspector]

    public enum PlayerStates { alive,dying,dead};

    [HideInInspector]

    public PlayerStates actualPlayerState;

    public float respawnTime,diyingTime;

    [Header("Speed Settings")]
    public float lowJumpVelocity;
    public float normalJumpVelocity;
    public float highJumpVelocity;
    public float fallMultiplier = 2.5f;
    public float playerSpeed = 5f;
    public float lowJumpMultiplier = 2f;
    
    public float shakeAmt;

    private Vector3 originalPlayerPos;

    [Header("Player Beat Settings")]

    public float errorRange;

    private float jumpPressedTime = -1, downBeatTime, normalBeatTime;
    private float move;
    
    [HideInInspector]

    public Rigidbody2D rb;

    [HideInInspector]

    public SpriteRenderer myrenderer;

    [HideInInspector]

    public CheckPointScript playerCheckPoint;

	[HideInInspector]

	public Animator Anim;

    public float drag = 0.01f;

	//public SpriteRenderer run;

    // Use this for initialization
    
    void Start()
    {
        JumpedWhenGrounded = false;
        manageDownBeat = false;
        JumpedOnDownBeat = false;
        rb = GetComponent<Rigidbody2D>();
        myrenderer = GetComponent<SpriteRenderer>();
        ChangePlayerState(PlayerStates.alive);
		Anim = GetComponent<Animator> ();
    }
    
    // Update is called once per frame
    void Update()
    {
        //Vector3 testCameraPos = Camera.main.transform.position;
        //testCameraPos.x = transform.position.x + 4;
        //Camera.main.transform.position = testCameraPos;



        switch (actualPlayerState)
        {
            case PlayerStates.alive:

                ManageMove();
                ManageJump();

                ManagePlayerFall();



                //Debug.Log(rb.velocity);



                break;

		case PlayerStates.dying:

			Vector3 newPos = originalPlayerPos + (Random.insideUnitSphere * (Time.deltaTime * shakeAmt));
			newPos.y = transform.position.y;
			newPos.z = transform.position.z;
			transform.position = newPos;
			Anim.SetBool ("dying", true);
                break;

            case PlayerStates.dead:
			Anim.SetBool ("dying", false);
                break;

        }
			

		Anim.SetBool ("grounded", grounded);
		Anim.SetFloat ("speedX", rb.velocity.x);
		Anim.SetFloat ("speedY", rb.velocity.y);
        
      
      

        //if (BeatManager.currentBeat == BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat && grounded)
        //{ 
        //    BeatAnimation.Play();
        //}
        
    }
		
    public void ChangePlayerState(PlayerStates newState)
    {

        switch (newState)
        {
            case PlayerStates.alive:

                myStream.Play();
                //myrenderer.enabled = true;
                rb.isKinematic = false;

                if (!(GameManagerScript.actualScreen == playerCheckPoint.myScreen))
                {
                    GameManagerScript.actualScreen.ChangeScreen(playerCheckPoint.myScreen);
                }



                break;

            case PlayerStates.dying:
                diyingSound.Play();
                originalPlayerPos = transform.position;
                myStream.Stop();
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                StartCoroutine(PlayerCounter(diyingTime, PlayerStates.dead));

                break;

            case PlayerStates.dead:

                explosionSound.Play();

                //myrenderer.enabled = false;

                transform.position = originalPlayerPos;

                Instantiate(DeadParticles, transform.position, transform.rotation);

  

                transform.position = playerCheckPoint.transform.position;

                StartCoroutine(PlayerCounter(respawnTime, PlayerStates.alive));

                break;

        }


        actualPlayerState = newState;
    }

    public void ManageJump()
    {
        if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat)
        {
            JumpedWhenGrounded = false;
            JumpedOnDownBeat = false;
            //rb.velocity = Vector2.zero;
            downBeatTime = Time.time;
            manageDownBeat = true;
            if (grounded) { MakeJump(normalJumpVelocity, false); JumpedWhenGrounded = true; }
            //Debug.Log("Down beat: " + downBeatTime);


        }
        else if (BeatManager.currentBeat == BeatManager.BeatType.FourthBeat)
        {

            
            
            //MakeJump(normaljumpVelocity);
            manageDownBeat = false;
            normalBeatTime = Time.time;
            //Debug.Log("Normal Beat: " + normalBeatTime);
            
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            jumpPressedTime = Time.time;
            //Debug.Log("Jump press: " + jumpPressedTime);
        }

        if (manageDownBeat)
        {
            if (jumpPressedTime != -1 && Mathf.Abs(jumpPressedTime - downBeatTime) < errorRange && !JumpedOnDownBeat && JumpedWhenGrounded)
            {
                float actualYVelocity = 0;
                if (jumpPressedTime <= downBeatTime)
                {
                    actualYVelocity = highJumpVelocity-rb.velocity.y;
                }
                else
                {
                   

                    actualYVelocity = (highJumpVelocity + (Physics.gravity.y * (Time.time - downBeatTime)))- rb.velocity.y;
                }

                

                //if (!grounded) { rb.velocity = new Vector2(rb.velocity.x, 0); }
                //Debug.Log(downBeatJumpVelocity);
                if (actualYVelocity < 0) { actualYVelocity = 0; }
               
                //Debug.Log(actualYVelocity);

                MakeJump(actualYVelocity);

                JumpedOnDownBeat = true;

                //Debug.Log("DOWN BEAT JUMP");

            }
           
        }
        else
        {
            if (jumpPressedTime != -1 && Mathf.Abs(jumpPressedTime - normalBeatTime) < errorRange && grounded)
            {
                //rb.velocity = Vector2.zero;
                MakeJump(lowJumpVelocity);
                //Debug.Log("Normal Beat Jump");
            }
        }

        
            
    }

    public void MakeJump(float jumpVelocity, bool fromPlayer = true)
    {

        //rb.velocity = Vector2.zero;
        //Debug.Log("InJUMP  " + (Vector2.up * jumpVelocity));

        if (fromPlayer)
            jumpPressedTime = -1;
        //Debug.Log("RESULT: " + (rb.velocity+ Vector2.up * jumpVelocity));

        rb.velocity += Vector2.up * jumpVelocity;
        grounded = false;

        if (transform.parent != null)
            transform.parent = null;

        if(fromPlayer)
        jumpSound.Play();

    }

    public void ManagePlayerFall()
    {
        if (rb.velocity.y != 0)
        {
            grounded = false;

            if (transform.parent != null)
                transform.parent = null;

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }

            else if (rb.velocity.y > 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        else
        {
            grounded = true;
        }
    }

    public void ManageMove()
    {
        
        float axis=Input.GetAxis("Horizontal");
		FlipAnimation (axis);
       

        move = axis * (playerSpeed * 2);

        if (grounded)
        {
            drag = 0.5f;
        }

        else
        {
            drag = 0.01f;
        }

        var vel = rb.velocity;

        vel.x *= 1 - drag;

        if (Mathf.Abs(vel.x) <= playerSpeed * 2 && axis != 0)
        {
            vel.x = move;
        }

        rb.velocity = vel;
    }

	private void FlipAnimation(float axis)
	{
		if(axis > 0) 
		{
			//run.flipX = false;	
		}
		if(axis<0)
		{
			//run.flipX = true;
		}
	}


    private void OnCollisionEnter2D(Collision2D col)
    {
        if((col.gameObject.tag == "Enemy"||col.gameObject.tag == "Spike")&& actualPlayerState == PlayerStates.alive)
        {
            ChangePlayerState(PlayerStates.dying);
        }
        
        /*else
        {
            grounded = true;
        }*/
	}

    IEnumerator PlayerCounter (float TotalTime,PlayerStates nextState)
    {
        yield return new WaitForSeconds(TotalTime);

        ChangePlayerState(nextState);

      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Spike")&&actualPlayerState==PlayerStates.alive)
        {
            ChangePlayerState(PlayerStates.dying);
        }

        else if (collision.gameObject.tag == "Crystal")
        {
            GameManagerScript.actualScreen.LevelEndingDoor.SetActive(false);
        }

        else if (collision.gameObject.tag == "Platform")
        {
            transform.parent = collision.gameObject.transform;
        }

        else

        {
            grounded = true;
        }
    }
}