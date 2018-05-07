using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : BeatActor {

	public float duration, mag;

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		SetBehavior();
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (BeatListener()) {
			print ("shakeando");
			StartCoroutine (Shake(duration, mag));
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
