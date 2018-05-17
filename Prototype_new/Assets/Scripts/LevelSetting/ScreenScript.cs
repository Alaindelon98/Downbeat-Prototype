using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScreenScript : MonoBehaviour
{
	public ScreenScript upScreen, downScreen, leftScreen, rightScreen;
    public ScreenSettings myScreenSettings;
	public float maxSize, minSize;
	public float zoomVariation=0.05f;


    public GameObject LevelEndingDoor;

   // public List<FallingRockScript> FallingRocks;
    private bool HasRocks;
    private int rockIndex = 0;
	// Use this for initialization
	void Start () 
	{
       

		maxSize = Camera.main.orthographicSize;
		minSize = maxSize - zoomVariation;
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
        myScreenSettings.positionScreen = Camera.main.transform.position;
        myScreenSettings.sizeScreen = Camera.main.orthographicSize;
    }
    public void CallCamera()
    {
        
        Camera.main.transform.position = myScreenSettings.positionScreen;

        Camera.main.orthographicSize = myScreenSettings.sizeScreen;
    }
}

[System.Serializable]

public class ScreenSettings
{
    public float sizeScreen;
    public Vector3 positionScreen;

}

