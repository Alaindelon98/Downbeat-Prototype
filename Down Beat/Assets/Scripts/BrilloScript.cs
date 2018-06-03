using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrilloScript : BeatActor {

	public Tilemap tilemap;

	private Color original;

	// Use this for initialization
	void Start () {
		SetBehavior();
		original = tilemap.color;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (BeatListener ()) {
			tilemap.color = original;
		}
		else
		{
			tilemap.color = new Color (original.r, original.g, original.b, 0.75f);
		}*/
		if (BeatManager.fourthNotesCounter == 1) {
			tilemap.color = original;
		} else {
			tilemap.color = new Color (original.r, original.g, original.b, 0.75f);
		}
	}
}
