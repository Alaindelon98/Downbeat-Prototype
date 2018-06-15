using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionScipt : MonoBehaviour {

	public AudioSource song;
	public Animator animator;

	private int index;
	// Use this for initialization
	void Start () {
		index = SceneManager.GetActiveScene ().buildIndex;
	}
	
	// Update is called once per frame
	void Update () {
		if (song.time >=5.5f) {
			FadeToLevel();
		}
	}
	public void FadeToLevel()
	{
		animator.SetTrigger ("EndLevel");
	}
	public void OnFadeComplete()
	{
		SceneManager.LoadScene (index + 1);
	}
}
