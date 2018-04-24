using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour {

    public float bpm;
    public int beatUnit = 4;
    public int barLength = 4;
    public int strongBeat = 1;
    float bps;
    AudioSource mySource;
    public AudioClip downBeat, offBeat;
    public Jump playerJump;
    public float beatPos;
    public static BeatType beatType;
    public float nextTimeSample;
    public float delay;
	// Use this for initialization

	void Awake () {
        mySource = GetComponent<AudioSource>();
        beatPos = 1;
        bps = bpm / 60;

        nextTimeSample = delay * mySource.clip.frequency;

        //StartCoroutine(SyncToAudio());

    }

    // Update is called once per frame
    void PlayBeat()
    {

        Debug.Log(beatPos);

        if (beatPos == strongBeat)
        {

            mySource.PlayOneShot(downBeat);


        }
        else
        {
            mySource.PlayOneShot(offBeat);


        }

        if (beatPos == barLength)
        {
            beatPos = 0;
        }

        if (beatPos == strongBeat)
        {
            playerJump.downBeat = true;
        }


        beatPos++;
    }

        
    void FixedUpdate () {

        if (mySource.timeSamples >= nextTimeSample)
        {
            PlayBeat();
            nextTimeSample += 1 / bps * mySource.clip.frequency;
        }
    }

    /*IEnumerator SyncToAudio()
    {
        while (mySource.isPlaying)
        {
            while (mySource.timeSamples < nextTimeSample) yield return 0; // wait till the desired sample

            PlayBeat();
            nextTimeSample += nextTimeSample += 1 / bps * mySource.clip.frequency;
        }
    }*/
}

public enum BeatType {
		None		= 0,
		OffBeat		= 1,
		OnBeat		= 2,
		UpBeat		= 4,
		DownBeat	= 8
	};