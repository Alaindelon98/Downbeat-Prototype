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

    public float fallMultiplier = 2.5f;
    public float playerSpeed = 5f;
    public float lowJumpMultiplier = 2f;
    public float errorRange;

    float jumpPressedTime = -1, downBeatTime, normalBeatTime;
    float move;

    public Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        JumpedWhenGrounded = false;
        manageDownBeat = false;
        JumpedOnDownBeat = false;
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!grounded && rb.velocity.y == 0) { Debug.Log(transform.position); }
        //Debug.Log("FPS: " + 1.0f / Time.deltaTime);
        //Vector3 testCameraPos = Camera.main.transform.position;
        //testCameraPos.x = transform.position.x + 4;
        //Camera.main.transform.position = testCameraPos;

        if (BeatManager.currentBeat == BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat && grounded)
        { 
            BeatAnimation.Play();
        }



        move = Input.GetAxis("Horizontal") * (playerSpeed * 2);

        ManageJump();

        ManagePlayerFall();

        rb.velocity = new Vector2(move, rb.velocity.y);

        //Debug.Log(grounded);
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
        Debug.Log("InJUMP  " + (Vector2.up * jumpVelocity));

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
            GameManagerScript.PlayerDeath();
        }
        else
        {
            grounded = true;
        }
	}

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Spike")
    }*/
}
