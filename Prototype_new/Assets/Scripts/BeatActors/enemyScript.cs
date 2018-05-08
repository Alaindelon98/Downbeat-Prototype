using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : BeatActor {


	public float speed,jumpVelocity;
    public Rigidbody2D myRigidbody;
	public int direction=1;
    public SpriteRenderer sprite;
    private bool grounded;
    private float drag = 0.05f;

	// Use this for initialization
	void Start ()
    {
        SetBehavior();
        sprite = this.GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();

        myRigidbody.AddForce(new Vector2(direction * speed, 0));
	}

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update () 
	{
        if (!actOnBeat)
        {
            return;
        }

        if (BeatListener() && grounded)
        {
            Jump();
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

        if (Mathf.Abs(vel.x) < Mathf.Abs(direction * speed))
            vel.x = direction * speed;


        myRigidbody.velocity = vel;

        //myRigidbody.velocity = new Vector2(direction * speed, myRigidbody.velocity.y);

        //saltito 

        //Debug.Log(myRigidbody.velocity);
	}
    private void Jump()
    {
        myRigidbody.velocity = Vector2.up * jumpVelocity;
        grounded = false;
        PlaySound();
    }

   
}
