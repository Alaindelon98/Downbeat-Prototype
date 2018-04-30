using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherScript : BeatActor
{
    public float PushForce;
    public Animation BeatAnim;
    private bool bumped;

    

	// Use this for initialization
	void Start ()
    {
        bumped = false;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (BeatListener()&&!bumped)
        {
            BeatAnim.Play();
            PlaySound();
            bumped = true;
        }

        if(BeatManager.currentBeat == BeatManager.BeatType.NoBeat)
        {
            bumped = false;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player"|| col.gameObject.tag == "Enemy")
        {
            if (BeatListener()&&!bumped)
            {
                col.GetComponent<Rigidbody2D>().velocity += Vector2.up * PushForce;
                
            }
        }
    }
}
