using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    public float normaljumpVelocity, downBeatVelocity, downBeatJumpVelocity;
    public bool grounded;
    public AudioSource jumpSound;
    public Animation BeatAnimation;

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
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {

        Vector3 testCameraPos = Camera.main.transform.position;
        testCameraPos.x = transform.position.x + 4;
        Camera.main.transform.position = testCameraPos;

        if (BeatManager.currentBeat == BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat && grounded)
        { 
            BeatAnimation.Play();
        }


        //if (grounded)
        //{
        //    move = Input.GetAxis("Horizontal") * playerSpeed;
        //}
        //else
        //{
        //    move = Input.GetAxis("Horizontal") * (playerSpeed * 2);
        //}

        move = Input.GetAxis("Horizontal") * (playerSpeed * 2);

        ManageJump();

        ManagePlayerFall();

        rb.velocity = new Vector2(move, rb.velocity.y);

    }

    public void ManageJump()
    {
        /*if(BeatManager.currentBeat == BeatManager.BeatType.DownBeat || BeatManager.currentBeat == BeatManager.BeatType.FourthBeat)
        {
            jumpTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Jump"))
        {
            jumpPressedTime = Time.time;
            //Debug.Log("Jump pressed time: " + jumpPressedTime);
            
            if (Mathf.Abs(jumpPressedTime - jumpTime) < afterErrorRange && grounded)
            {
                rb.velocity = Vector2.zero;
                if (BeatManager.fourthNotesCounter == 1)
                {
                    MakeJump(normaljumpVelocity + beatJumpVelocity / 3 * 2);
                    Debug.Log("Long jump AFTER downBeat");

                }
                else
                {
                    MakeJump(normaljumpVelocity);
                    Debug.Log("Long jump AFTER normalbeat");
                }

                jumpSound.Play();
            }
        }

        if ((BeatManager.currentBeat == BeatManager.BeatType.DownBeat || BeatManager.currentBeat == BeatManager.BeatType.FourthBeat) && Mathf.Abs(jumpPressedTime - Time.time) < previousErrorRange && grounded)
        {
            if(!grounded)
                rb.velocity = Vector2.zero;

            MakeJump(normaljumpVelocity + beatJumpVelocity / 3 * 2);
            Debug.Log("Long jump Before");

            jumpSound.Play();
        }

        else if ((BeatManager.currentBeat == BeatManager.BeatType.DownBeat))
        {
            MakeJump(beatJumpVelocity);
            //Debug.Log("Jump time: " + jumpTime);
        }*/
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            jumpPressedTime = Time.time; 
        }
        if (jumpPressedTime != -1)
        {
            if (Mathf.Abs(jumpPressedTime - downBeatTime) < errorRange)
            {
                MakeJump(downBeatJumpVelocity);
                Debug.Log("DOWN BEAT JUMP");
            }
            else if (Mathf.Abs(jumpPressedTime - normalBeatTime) < errorRange && grounded)
            {
                rb.velocity = Vector2.zero;
                MakeJump(normaljumpVelocity);
                Debug.Log("Normal Beat Jump");
            }
        }

        if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat)
        {
            //rb.velocity = Vector2.zero;
            downBeatTime = Time.time;
            if(grounded)
            {
                MakeJump(downBeatVelocity, false);
                Debug.Log("Auto Down Beat");

            }

        }
        else if(BeatManager.currentBeat == BeatManager.BeatType.FourthBeat)
        {
            //MakeJump(normaljumpVelocity);
            normalBeatTime = Time.time;
        }

    }
    public void MakeJump(float jumpVelocity, bool fromPlayer = true)
    {
        rb.velocity = Vector2.zero;

        if (fromPlayer)
            jumpPressedTime = -1;

        rb.velocity = Vector2.up * jumpVelocity;
        grounded = false;

        if(fromPlayer)
        jumpSound.Play();

    }

    public void ManagePlayerFall()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            GameManagerScript.PlayerDeath();
        }
        else
        {
            grounded = true;
        }
	}

}
