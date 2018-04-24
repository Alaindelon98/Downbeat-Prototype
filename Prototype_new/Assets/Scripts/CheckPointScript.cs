using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public ScreenScript myScreen;
    public bool checkPointActive;

	// Use this for initialization
	void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (checkPointActive)
        {
        }
		
	}
    void OnTriggerEnter2D()
    {
        checkPointActive = true;
        GameManagerScript.playerCheckPoint = this;
    }
}
