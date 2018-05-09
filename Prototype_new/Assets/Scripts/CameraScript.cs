using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : BeatActor {

	public float duration, mag;
	//public float minZoom, maxZoom;

	public Animation zoom;

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		SetBehavior();
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (BeatListener ()) 
		{
			zoom.Stop ();
			StartCoroutine (Shake (duration, mag));
		} 
		else if(BeatManager.fourthNotesCounter == 2)
		{
			zoom.Play ();

			//Camera.main.orthographicSize = minZoom;
			//Camera.main.orthographicSize = maxZoom;
		}
		else if(BeatManager.fourthNotesCounter == 3)
		{
			zoom.Play ();
		}
		else if (BeatManager.fourthNotesCounter == 4)
		{
			zoom.Play ();
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

}
