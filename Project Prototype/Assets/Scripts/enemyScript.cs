using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {


	public float speed;
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
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
        direction *= -1;
        transform.localScale *= -1;
	}

	private void Mov()
	{
        myRigidbody.velocity = new Vector3(direction * speed * Time.deltaTime, 0);
		//saltito 
	}
}
