using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {


	public float speed,jumpVelocity;
    public Rigidbody2D myRigidbody;
	private int direction=1;
    public SpriteRenderer sprite;

	// Use this for initialization
	void Start ()
    {
        sprite = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Mov ();
        if (BeatManager.OnBeat && BeatManager.fourthNotesCounter == 1) { Jump(); }
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.gameObject.tag =="EnemyLimit")
        {
            direction *= -1;
            transform.localScale *= -1;
        }
      
	}

	private void Mov()
	{
        myRigidbody.velocity = new Vector3(direction * speed, 0);
		//saltito 
	}
    private void Jump()
    {
        myRigidbody.velocity += new Vector2(0,jumpVelocity);
    }
}
