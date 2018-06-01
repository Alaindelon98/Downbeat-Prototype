using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ScreenScript : MonoBehaviour
{
	public ScreenScript upScreen, downScreen, leftScreen, rightScreen;
    public ScreenSettings myScreenSettings;
	public float maxSize, minSize;
	public float zoomVariation = 0.05f;
    public bool startMuffledSong = false;
    public bool returnFromMuffled = false;

    private FallingRockScript rocksManager;

    public GameObject LevelEndingDoor;

   // public List<FallingRockScript> FallingRocks;
    private bool HasRocks;
    private int rockIndex = 0;
	// Use this for initialization
	void Start () 
	{
        rocksManager = GetComponentInChildren<FallingRockScript>();


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
            case "End":
                ChangeLevel();
                break;
        }
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(GameManagerScript.currentScene + 1);
    }

	public void ChangeScreen(ScreenScript newScreen)
	{
        if (rocksManager != null)
            rocksManager.ResetRocks();

        if(newScreen.startMuffledSong)
        {
            BeatManager.ChangeSong(true);
        }

        else if(newScreen.returnFromMuffled)
        {
            BeatManager.ChangeSong(false);
        }

        newScreen.CallCamera();

        newScreen.gameObject.SetActive(true);

        gameObject.SetActive(false);


		
        GameManagerScript.actualScreen = newScreen;
	}
    public void TakeMeasures()
    {
        myScreenSettings.positionScreen = Camera.main.transform.position;
        myScreenSettings.sizeScreen = Camera.main.orthographicSize;
        //transform.position = Camera.main.transform.position;
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

