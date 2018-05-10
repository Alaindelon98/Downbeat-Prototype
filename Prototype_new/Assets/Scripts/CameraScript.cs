using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : BeatActor {

	public float duration, mag;

	private bool zoomActive = false;
	public Animation zoom;

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		SetBehavior();
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat) {
			StartCoroutine (Shake (duration, mag));
		} 
		/*else if (BeatManager.fourthNotesCounter == 2) 
		{
			if (BeatListener () && !zoomActive) {
				StartCoroutine (ZoomIn ());

				print ("aiiix");
				zoom.Stop ();
				//StartCoroutine (Shake (duration, mag));
			} else if (BeatListener () && zoomActive) {
				StartCoroutine (ZoomOut ());
			}
		}*/

		/*else if(BeatManager.fourthNotesCounter == 2 && !zoomActive)
		{
			print ("uuuuuuuuh");
			//ZoomIn ();
			//zoomActive = true;
			//ZoomOut ();
			//zoom.Play ();

			//Camera.main.orthographicSize = minZoom;
			//Camera.main.orthographicSize = maxZoom;
			 
			StartCoroutine (ZoomIn());
		}
		else if(BeatManager.fourthNotesCounter == 2 && zoomActive)
		{
			StartCoroutine (ZoomOut());

		}
		else if(BeatManager.fourthNotesCounter == 3 && !zoomActive)
		{
			StartCoroutine (ZoomIn());

			//ZoomIn ();
			//ZoomOut ();

			//zoom.Play ();
		}
		else if(BeatManager.fourthNotesCounter == 3 && zoomActive)
		{
			StartCoroutine (ZoomOut());

			//ZoomOut ();
		}
		else if (BeatManager.fourthNotesCounter == 4 && !zoomActive)
		{
			StartCoroutine (ZoomIn());

			//ZoomIn ();
			//ZoomOut ();

			//zoom.Play ();
		}
		else if(BeatManager.fourthNotesCounter == 4 && zoomActive)
		{
			StartCoroutine (ZoomOut());

			//ZoomOut ();
		}*/
	}

	/*public void ZoomIn()
	{
		while (Camera.main.orthographicSize > minZoom)
		{
			Camera.main.orthographicSize -= 0.01f;
		}
	}
	public void ZoomOut()
	{
		while(Camera.main.orthographicSize < maxZoom)
		{
			Camera.main.orthographicSize += 0.01f;
		}
	}*/

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
		zoomActive = true;
	}

	IEnumerator ZoomOut()
	{
		while(Camera.main.orthographicSize < GameManagerScript.actualScreen.maxSize)
		{
			Camera.main.orthographicSize += 0.01f;
			yield return null;
		}
		zoomActive = false;
	}
}
