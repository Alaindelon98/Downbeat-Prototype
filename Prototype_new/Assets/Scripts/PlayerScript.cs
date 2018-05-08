using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    public float normaljumpVelocity, downBeatVelocity, downBeatJumpVelocity;
    public bool grounded;
    public AudioSource jumpSound;
    public Animation BeatAnimation;
    private bool manageDownBeat,JumpedOnDownBeat,JumpedWhenGrounded;
    public ParticleSystem myStream;
    public GameObject DeadParticles;

    public enum PlayerStates { alive,dying,dead};
    public PlayerStates actualPlayerState;

    public float respawnTime,diyingTime;

    public float fallMultiplier = 2.5f;
    public float playerSpeed = 5f;
    public float lowJumpMultiplier = 2f;
    public float errorRange;
    public float shakeAmt;

    private Vector3 originalPlayerPos;

    float jumpPressedTime = -1, downBeatTime, normalBeatTime;
    float move;

    public Rigidbody2D rb;
    public SpriteRenderer myrenderer;
    public CheckPointScript playerCheckPoint;

    // Use this for initialization
    void Start()
    {
        JumpedWhenGrounded = false;
        manageDownBeat = false;
        JumpedOnDownBeat = false;
        rb = GetComponent<Rigidbody2D>();
        myrenderer = GetComponent<SpriteRenderer>();
        ChangePlayerState(PlayerStates.alive);

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

                move = Input.GetAxis("Horizontal") * (playerSpeed * 2);

                ManageJump();

                ManagePlayerFall();

                rb.velocity = new Vector2(move, rb.velocity.y);

                break;

            case PlayerStates.dying:

                Vector3 newPos = originalPlayerPos + (Random.insideUnitSphere * (Time.deltaTime * shakeAmt));
                newPos.y = transform.position.y;
                newPos.z = transform.position.z;
                transform.position = newPos;

                break;

            case PlayerStates.dead:

                break;

        }

        
      
      

        //if (BeatManager.currentBeat == BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat && grounded)
        //{ 
        //    BeatAnimation.Play();
        //}



       

        //Debug.Log(grounded);
    }

    public void ChangePlayerState(PlayerStates newState)
    {

        switch (newState)
        {
            case PlayerStates.alive:

                myStream.Play();
                myrenderer.enabled = true;
                rb.isKinematic = false;

                if (!(GameManagerScript.actualScreen == playerCheckPoint.myScreen))
                {
                    GameManagerScript.actualScreen.ChangeScreen(playerCheckPoint.myScreen);
                }



                break;

            case PlayerStates.dying:

                originalPlayerPos = transform.position;
                myStream.Stop();
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                StartCoroutine(PlayerCounter(diyingTime, PlayerStates.dead));

                break;

            case PlayerStates.dead:

                myrenderer.enabled = false;

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
            if (grounded) { MakeJump(downBeatVelocity, false); JumpedWhenGrounded = true; }
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
                    actualYVelocity = downBeatJumpVelocity-rb.velocity.y;
                }
                else
                {
                   

                    actualYVelocity = (downBeatJumpVelocity + (Physics.gravity.y * (Time.time - downBeatTime)))- rb.velocity.y;
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
                MakeJump(normaljumpVelocity);
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

        if(fromPlayer)
        jumpSound.Play();

    }

    public void ManagePlayerFall()
    {
        if (rb.velocity.y != 0)
        {
            grounded = false;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy"||col.gameObject.tag == "Spike")
        {
            ChangePlayerState(PlayerStates.dying);
        }
        
        else
        {
            grounded = true;
        }
	}

    IEnumerator PlayerCounter (float TotalTime,PlayerStates nextState)
    {
        yield return new WaitForSeconds(TotalTime);

        ChangePlayerState(nextState);

      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crystal")
        {
            GameManagerScript.actualScreen.LevelEndingDoor.SetActive(false);
        }
    }
}