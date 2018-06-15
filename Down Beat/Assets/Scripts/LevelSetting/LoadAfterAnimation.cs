using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterAnimation : MonoBehaviour {

    public AudioSource BoomSound;

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void OnEnable()
    {

        Debug.Log("PrintOnEnable: script was enabled");

        StartCoroutine(StarLowingVolume());
    }

    public void LoadNext()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == 10)
        {
            nextSceneIndex = 0;
        }
        //Debug.Log(nextSceneIndex);

        SceneManager.LoadScene(nextSceneIndex);
    }
    IEnumerator StarLowingVolume()
    {

        while (BeatManager.mySource.volume > 0)
        {
            BeatManager.mySource.volume -= 0.005f;
        }
        yield return new WaitForSeconds(3);

        LoadNext();

    }
}

