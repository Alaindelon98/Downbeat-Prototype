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
    public AudioSource EndingLevel;
    public Animator FadeAnimator;

    public float timeToChange;

    private FallingRockScript rocksManager;

    public GameObject LevelEndingDoor;

    public GameObject spectrum;
   

   // public List<FallingRockScript> FallingRocks;
    private bool HasRocks;
    private int rockIndex = 0;
	// Use this for initialization
	void Start () 
	{
        rocksManager = GetComponentInChildren<FallingRockScript>();

        spectrum = Camera.main.transform.GetChild(1).gameObject;
        maxSize = Camera.main.orthographicSize;
		minSize = maxSize - zoomVariation;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        if (Input.GetKeyDown(KeyCode.P))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(nextSceneIndex);
        }        
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
                EndingLevel.Play();
                FadeAnimator.SetTrigger("EndLevel");
                GameManagerScript.player.ChangePlayerState(PlayerScript.PlayerStates.ending);
                 StartCoroutine(ChangeLevel());
                break;
        }
    }

    public IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(timeToChange);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex == 5)
        {
            nextSceneIndex = 0;
        }
        //Debug.Log(nextSceneIndex);

        SceneManager.LoadScene(nextSceneIndex);

        //else
        //{
        //    SceneManager.LoadScene(0);
        //}
    }

	public void ChangeScreen(ScreenScript newScreen)
	{
        if (rocksManager != null)
            rocksManager.ResetRocks();

        if(newScreen.startMuffledSong)
        {
            BeatManager.ChangeSong(true);
            spectrum.SetActive(false);
        }

        else if(newScreen.returnFromMuffled)
        {
            BeatManager.ChangeSong(false);
            spectrum.SetActive(true);
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

