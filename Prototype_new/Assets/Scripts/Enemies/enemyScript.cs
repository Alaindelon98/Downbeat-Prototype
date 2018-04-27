using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : BeatActor {


	public float speed,jumpVelocity;
    public Rigidbody2D myRigidbody;
	public int direction=1;
    public SpriteRenderer sprite;
    private bool grounded;

	// Use this for initialization
	void Start ()
    {
        SetBehavior();
        sprite = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move ();

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
	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag =="EnemyLimit")
        {
            direction *= -1;
            transform.localScale *= -1;
        }
       
      
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        grounded = true;
    }

	private void Move()
	{
        myRigidbody.velocity = new Vector2(direction * speed, myRigidbody.velocity.y );
		//saltito 
	}
    private void Jump()
    {
        myRigidbody.velocity = Vector2.up * jumpVelocity;
        grounded = false;
    }
}
