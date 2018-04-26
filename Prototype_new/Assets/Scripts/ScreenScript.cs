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
		/*switch (tagcol) 
		{
		case "Left":
			ChangeScreen (leftScreen);
			break;
		case "Right":
			ChangeScreen (rightScreen);
			break;
		case "Top":
			ChangeScreen (upScreen);
			break;
		case "Down":
			ChangeScreen (downScreen);
			break;
		}*/
	}

	public void ChangeScreen(ScreenScript newScreen)
	{
        Vector3 newPos = newScreen.positionScreen;
        newPos.z = -10;
        Camera.main.transform.position = newPos;

		Camera.main.orthographicSize= newScreen.sizeScreen;

        newScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);

		
        GameManagerScript.actualScreen = newScreen;
	}
}
