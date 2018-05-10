using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherScript : BeatActor
{
    public float VerticalPushForce,HorizontalPushForce;
    public Animation BeatAnim;
    private bool bump,bumped;

    

	// Use this for initialization
	void Start ()
    {
        SetBehavior();

        bump = false;
        bumped = false;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (BeatListener())
        {
            BeatAnim.Play();
            //PlaySound();
            bump = true;
        }

        if (BeatManager.currentBeat == BeatManager.BeatType.NoBeat)
        {
            bump = false;
            bumped = false;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
       // Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag=="Player"|| col.gameObject.tag == "Enemy")
        {
            //Debug.Log(col.gameObject.tag+"inside");

            if (bump && !bumped)
            {

                Rigidbody2D colRb = col.gameObject.GetComponent<Rigidbody2D>();
                //colRb.velocity = new Vector2(colRb.velocity.x+ HorizontalPushForce, VerticalPushForce);

                colRb.AddForce(new Vector2(HorizontalPushForce,VerticalPushForce));
                col.gameObject.GetComponent<PlayerScript>().impulsed = true;
             
                bump = false;
               // Debug.Log("Impulsed");
                bumped = true;
                
            }
        }
    }
}
