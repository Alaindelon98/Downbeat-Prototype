using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsToBeat : BeatActor {

    public Animation BuildRythm;

	// Use this for initialization
	void Start ()
    
    {
        SetBehavior();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (BeatListener())
        {
            BuildRythm.Play();
        }
		
	}
}
