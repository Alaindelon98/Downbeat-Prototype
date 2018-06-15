using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrilloScript : BeatActor {

	public Tilemap tilemap;
    public SpriteRenderer renderer;
	private Color original;
	private float menosAlpha;
    public bool IsSpriteRenderer = false;

	// Use this for initialization
	void Start () {
      
		SetBehavior();
        if (!IsSpriteRenderer) { original = tilemap.color; }
        else { original = renderer.color; }
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
		if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat) {
            if (IsSpriteRenderer)
            {
                renderer.color = original;
            }
            else
            {
                tilemap.color = original;
            }
			
			menosAlpha = 0;
		} else {
			menosAlpha += 0.00015f;

            if (IsSpriteRenderer)
            {
                renderer.color = new Color(original.r, original.g, original.b, renderer.color.a - menosAlpha);
            }
            else
            {
                tilemap.color = new Color(original.r, original.g, original.b, tilemap.color.a - menosAlpha);
            }

           
		}
	}
}
