using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionScipt : MonoBehaviour {

	public AudioSource song;

	private int index;
	// Use this for initialization
	void Start () {
		index = SceneManager.GetActiveScene ().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		if (song.time >=6.0f) {
			SceneManager.LoadScene (index+1);
			//print("changeee");
		}
	}
}
