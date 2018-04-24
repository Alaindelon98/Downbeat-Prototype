using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

	public int directionH, directionV;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Mov ();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Left" || col.gameObject.tag == "Right" || col.gameObject.tag == "Up" || col.gameObject.tag == "Down") {

		}
	}

	private void Mov()
	{
		transform.position += new Vector3 (directionH * speed * Time.deltaTime, directionV * speed * Time.deltaTime, 0);
	}
	private void Die()
	{

	}
}
