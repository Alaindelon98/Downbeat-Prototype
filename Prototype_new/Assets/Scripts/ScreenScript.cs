using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScript : MonoBehaviour {


	public float sizeScreen;
	public Vector3 positionScreen;
	public ScreenScript upScreen, downScreen, leftScreen, rightScreen;


	// Use this for initialization
	void Start () 
	{
        positionScreen = positionScreen;
        sizeScreen = sizeScreen;

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
                ChangeScreen(leftScreen);
                break;
            case "Right":
                ChangeScreen(rightScreen);
                break;
            case "Top":
                ChangeScreen(upScreen);
                break;
            case "Down":
                ChangeScreen(downScreen);
                break;
        }
    }

	public void ChangeScreen(ScreenScript newScreen)
	{
        newScreen.CallCamera();

        newScreen.gameObject.SetActive(true);
        gameObject.SetActive(false);

		
        GameManagerScript.actualScreen = newScreen;
	}
    public void TakeMesures()
    {
        positionScreen = Camera.main.transform.position;
        sizeScreen = Camera.main.orthographicSize;
    }
    public void CallCamera()
    {
        
        Camera.main.transform.position = positionScreen;

        Camera.main.orthographicSize = sizeScreen;
    }
}
