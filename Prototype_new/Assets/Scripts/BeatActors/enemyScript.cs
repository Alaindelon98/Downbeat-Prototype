using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : BeatActor {


	public float speed,jumpVelocity;
    public Rigidbody2D myRigidbody;
	public int direction=1;
    public SpriteRenderer sprite;
    public bool onSpawner;
    private bool grounded;
    private float drag = 0.005f;

	// Use this for initialization
	void Start ()
    {
        SetBehavior();
        sprite = this.GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if(!onSpawner)
        myRigidbody.AddForce(new Vector2(direction * speed, 0));
	}

    private void FixedUpdate()
    {
        if (!onSpawner)
        {
            Move();

        }


    }

    // Update is called once per frame
    void Update () 
	{
        if (!onSpawner)
        {
            if (!actOnBeat)
            {
                return;
            }


            if (BeatListener() && grounded)
            {
                Jump();
            }
        }

        //Debug.Log(grounded);

        //if (shot && BeatManager.currentBeat == BeatManager.BeatType.NoBeat)
        //    shot = false;
        //if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat) { Jump(); }
    }
	
    private void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;

        if (col.gameObject.tag == "Spike")
        {
            Destroy(this.gameObject);
        }
    }

	private void Move()
	{

        var vel = myRigidbody.velocity;

        vel.x *= 1 - drag;

        if (Mathf.Abs(vel.x) < Mathf.Abs(direction * speed) && grounded)
            vel.x = direction * speed;


        myRigidbody.velocity = vel;

        //myRigidbody.velocity = new Vector2(direction * speed, myRigidbody.velocity.y);

        //saltito 

        //Debug.Log(myRigidbody.velocity);
	}
    private void Jump()
    {
        var vel = myRigidbody.velocity;

        vel.y = jumpVelocity;
        //myRigidbody.velocity = Vector2.up * jumpVelocity;
        myRigidbody.velocity = vel;
        grounded = false;
        PlaySound();
    }

   
}
