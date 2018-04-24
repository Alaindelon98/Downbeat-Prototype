using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    public float normaljumpVelocity,beatJumpVelocity;
    public bool grounded;
    public AudioSource jumpSound;
    public Animation BeatAnimation;

    public float fallMultiplier = 2.5f;
    public float playerSpeed = 5f;
    public float lowJumpMultiplier = 2f;
    public float previousErrorRange, afterErrorRange;

    float jumpPressedTime, jumpTime;
    float move;

    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (BeatManager.actualBeatType != BeatType.None && grounded ==true) { BeatAnimation.Play(); }
        if (BeatManager.OnBeat && grounded)
        { 
            BeatAnimation.Play();
        }


        //transform.position += new Vector3(Input.GetAxis("Horizontal")*playerSpeed*Time.deltaTime, 0, 0);

        move = Input.GetAxis("Horizontal") * playerSpeed;



        if ((BeatManager.fourthNotesCounter == 1 && BeatManager.OnBeat) && grounded)
        {
            MakeJump(beatJumpVelocity);
            jumpTime = Time.time;
            Debug.Log("Jump time: " + jumpTime);
        }

        ManageJump();

        ManagePlayerFall();

        rb.velocity = new Vector2(move, rb.velocity.y);

    }

    public void ManageJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressedTime = Time.time;
            Debug.Log("Jump pressed time: " + jumpPressedTime);

            if (Mathf.Abs(jumpPressedTime - jumpTime) < afterErrorRange)
            {
                if (!grounded)
                    rb.velocity = Vector2.zero;

                MakeJump(normaljumpVelocity + beatJumpVelocity / 3 * 2);
                Debug.Log("Long jump AFTER");
                jumpSound.Play();
            }
        }

        if ((BeatManager.fourthNotesCounter == 1 && BeatManager.OnBeat) && Mathf.Abs(jumpPressedTime - Time.time) < previousErrorRange && grounded)
        {
            if(!grounded)
                rb.velocity = Vector2.zero;

            MakeJump(normaljumpVelocity + beatJumpVelocity / 3 * 2);

            jumpSound.Play();
        }

    }
    public void MakeJump(float jumpVelocity)
    {
        rb.velocity = Vector2.up * jumpVelocity;
       
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

        //else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        //}
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        grounded = true;
        
		if (col.gameObject.tag == "Pantalla")
		{
			Camera.main.transform.position = col.gameObject.transform.position;
		}
    }
	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Pantalla")
		{
			Camera.main.transform.position = col.gameObject.transform.position;
		}
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Debug.Log("OhShitAmDed");
        }
	}

}
