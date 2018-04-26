using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelScript : MonoBehaviour {

    public PlayerScript player;
    public ScreenScript initialScreen;
    public CheckPointScript initialCheckPoint;

	// Use this for initialization
	void Start ()
    {
        GameManagerScript.LevelSetup(initialScreen, player, initialCheckPoint);

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
