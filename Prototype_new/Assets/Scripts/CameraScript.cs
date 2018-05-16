using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : BeatActor {

	public float duration, mag;

	//private bool zoomActive = false;
	public Animation zoom;

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		SetBehavior();
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (BeatListener ()) {
			StartCoroutine (Shake (duration, mag));
		}

		if(BeatManager.eighthNotesCounter == 3)
		{
			StartCoroutine (ZoomIn ());
		}
		if(BeatManager.eighthNotesCounter == 4)
		{
			StartCoroutine (ZoomOut ());
		}
		if(BeatManager.eighthNotesCounter == 5)
		{
			StartCoroutine (ZoomIn ());
		}
		if(BeatManager.eighthNotesCounter == 6)
		{
			StartCoroutine (ZoomOut ());
		}
		if(BeatManager.eighthNotesCounter == 7)
		{
			StartCoroutine (ZoomIn ());
		}
		if(BeatManager.eighthNotesCounter == 8)
		{
			StartCoroutine (ZoomOut ());
		}
	}

	IEnumerator Shake (float duration, float mag)
	{
		float counter = 0.0f;

		while (counter < duration) 
		{
			float x = Random.Range (-1f, 1f) * mag;
			float y = Random.Range (-1f, 1f) * mag;

			transform.position = new Vector3 (GameManagerScript.actualScreen.myScreenSettings.positionScreen.x + x,GameManagerScript.actualScreen.myScreenSettings.positionScreen.y + y, originalPos.z);

			counter += Time.deltaTime;

			yield return null;
		}

		transform.position = GameManagerScript.actualScreen.myScreenSettings.positionScreen;
	}

	IEnumerator ZoomIn()
	{
		while (Camera.main.orthographicSize > GameManagerScript.actualScreen.minSize)
		{
			Camera.main.orthographicSize -= 0.01f;
			yield return null;
		}
		//zoomActive = true;
	}

	IEnumerator ZoomOut()
	{
		while(Camera.main.orthographicSize < GameManagerScript.actualScreen.maxSize)
		{
			Camera.main.orthographicSize += 0.01f;
			yield return null;
		}
		//zoomActive = false;
	}

}
