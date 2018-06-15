using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlatformScript : BeatActor {

	public SpriteRenderer mySprite;

	private Color original;
	private float menosAlpha;

	// Use this for initialization
	void Start () 
	{
		SetBehavior();
		original = mySprite.color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat) {
			mySprite.color = original;
			menosAlpha = 0;
		} else {
			menosAlpha += 0.00015f;
			mySprite.color = new Color (original.r, original.g, original.b, mySprite.color.a - menosAlpha);
		}
	}
}
