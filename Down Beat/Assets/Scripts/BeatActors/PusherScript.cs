using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherScript : BeatActor
{
    public float VerticalPushForce,HorizontalPushForce;
    public Animation BeatAnim;
	public Animation touchAnim;
    private bool bump,bumped;
    private float BeatTime;
    public float JumpRange;

    

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
            BeatTime = Time.time;
            PlaySound();
            BeatAnim.Play();
            
            bump = true;
        }

        if (BeatManager.currentBeat == BeatManager.BeatType.NoBeat && (Time.time-BeatTime)>JumpRange)
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
                switch (col.gameObject.tag)
                {
				case "Player":
					if (GameManagerScript.player.actualPlayerState == PlayerScript.PlayerStates.alive) {
						Rigidbody2D colRb = col.gameObject.GetComponent<Rigidbody2D> ();
						colRb.velocity = new Vector2 (HorizontalPushForce, VerticalPushForce);

						//colRb.AddForce(new Vector2(HorizontalPushForce,VerticalPushForce));
						GameManagerScript.player.impulsed = true;
						GameManagerScript.player.jumpSound.Play ();
						TouchIt ();
					}
                        break;

                }
             
             
                bump = false;
               // Debug.Log("Impulsed");
                bumped = true;
                
            }
        }
    }

	private void TouchIt()
	{
		touchAnim.Play ();
	}
}
