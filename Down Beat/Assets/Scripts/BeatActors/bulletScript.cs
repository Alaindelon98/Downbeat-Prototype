using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

	public int directionH, directionV;
	public float speed;
    public Rigidbody2D rb;
	public SpriteRenderer mySprite;
    public GameObject DeadParticles;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 4);
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Enemy")
        {
            Instantiate(DeadParticles, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        
        
    }

	public void FlipY()
	{
		mySprite.flipY = true;
	}


    private void Move()
	{
		 rb.velocity = new Vector3 (directionH * speed, directionV * speed, 0);
	}
}
