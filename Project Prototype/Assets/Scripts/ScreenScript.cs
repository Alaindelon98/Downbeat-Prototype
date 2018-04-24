using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScript : MonoBehaviour {


	public int sizeScreen;
	public Vector3 positionScreen;
	public ScreenScript upScreen, downScreen, leftScreen, rightScreen;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void TypeScreen(string tagcol)
	{
		switch (tagcol) 
		{
		case "Left":
			ChangeScreen (leftScreen);
			break;
		case "Right":
			ChangeScreen (rightScreen);
			break;
		case "Up":
			ChangeScreen (upScreen);
			break;
		case "Down":
			ChangeScreen (downScreen);
			break;
		}
	}

	public void ChangeScreen(ScreenScript newScreen)
	{
        Camera.main.transform.position = newScreen.positionScreen;
		Camera.main.orthographicSize= newScreen.sizeScreen;
		
        foreach (Transform child in newScreen.transform)
        {
            child.transform.gameObject.SetActive(false);
        }

		foreach (Transform child in transform)
		{
			child.transform.gameObject.SetActive(false);
		}
        GameManagerScript.actualScreen = newScreen;
	}
}
