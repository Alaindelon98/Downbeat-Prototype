﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {



	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" && transform.parent.gameObject.activeSelf) 
		{
            if(transform.parent != null)
			gameObject.GetComponentInParent<ScreenScript> ().TypeScreen (this.tag);

		}
	}
}
