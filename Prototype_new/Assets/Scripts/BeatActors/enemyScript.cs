﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : BeatActor {


    public float speed;
    public Rigidbody2D myRigidbody;
	public int direction;
    public SpriteRenderer sprite;
    public bool onSpawner;
    private bool grounded;
    public float drag ;

	// Use this for initialization
	void Start ()
    {
        SetBehavior();
        sprite = this.GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if (!onSpawner) { }
        
	}

    private void FixedUpdate()
    {
        if (!onSpawner)
        {

            GestionateMovement();
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

            

            if (BeatListener())
            {
                Move(speed);

                PlaySound();

            }
        }

        //Debug.Log(grounded);

        //if (shot && BeatManager.currentBeat == BeatManager.BeatType.NoBeat)
        //    shot = false;
        //if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat) { Jump(); }
    }
	
    private void OnCollisionEnter2D(Collision2D col)
    {
      

        if (col.gameObject.tag == "Spike")
        {
            Destroy(this.gameObject);
        }
    }

	private void Move(float speedMove)
	{
        myRigidbody.velocity = Vector2.right * speedMove *direction;
       

        //var vel = myRigidbody.velocity;

        //vel.x *= 1 - drag;

        //if (Mathf.Abs(vel.x) < Mathf.Abs(direction * speed) && grounded)
        //    vel.x = direction * speed;


        //myRigidbody.velocity = vel;

        //myRigidbody.velocity = new Vector2(direction * speed, myRigidbody.velocity.y);

        //saltito 

        //Debug.Log(myRigidbody.velocity);
    }
    private void GestionateMovement()
    {
        if ((Mathf.Abs(myRigidbody.velocity.x)) > 0)
        {
            float xVel = Mathf.Abs( myRigidbody.velocity.x);
            xVel -= drag;
            Move(xVel);
        }

        //Debug.Log("actspeed"+myRigidbody.velocity.x);

        //Debug.Log("dir" + direction);
        //myRigidbody.velocity = Vector2.up * jumpVelocity;



    }

   
}
