﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

	public int directionH, directionV;
	public float speed;

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
		if (col.gameObject.tag == "Tilemap") {
			Destroy (this.gameObject);
		}
    }

	private void Move()
	{
		transform.position += new Vector3 (directionH * speed * Time.deltaTime, directionV * speed * Time.deltaTime, 0);
	}
}