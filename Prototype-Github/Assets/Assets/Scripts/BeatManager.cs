using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour {

    public float bpm;
    public int beatUnit = 4;
    public int barLength = 4;
    public int strongBeat = 1;
    private float bps;
    AudioSource mySource;
    public AudioClip downBeat, offBeat;
  
    public static int fourthNotesCounter;
    public static float eighthNotesCounter;
    public static bool OnBeat;
  
    public float nextFourthSample,nextEighthSample;
    public int delay;
	// Use this for initialization
	void Start ()
    {
        mySource = GetComponent<AudioSource>();
        fourthNotesCounter = barLength;
        eighthNotesCounter = barLength*2;
        bps = bpm / 60;

        nextFourthSample = delay;
        OnBeat = false;
    }

    // Update is called once per frame
    void PlayFourthBeat()
    {
        mySource.PlayOneShot(offBeat);

        if (fourthNotesCounter == barLength)
        {
            fourthNotesCounter = 1;
        }
        else
        {
            fourthNotesCounter++;
        }


        switch (fourthNotesCounter)
        {
            case 1:
             
                mySource.PlayOneShot(downBeat);

            break;
       
        }



       


    }
    void PlayEighthBeat()
    {
        nextEighthSample += 0.5f / bps * mySource.clip.frequency;

        eighthNotesCounter++;
        if (eighthNotesCounter == barLength)
        {
            eighthNotesCounter = 1;
        }
        else
        {
            eighthNotesCounter++;
        }
        
    }
    void Update ()
    {
      
      
        //Debug.Log(fourthNotesCounter);

        if (mySource.isPlaying)
        {
            if (mySource.timeSamples > nextFourthSample)
            {
                PlayFourthBeat();
                PlayEighthBeat();
                nextFourthSample += 1 / bps * mySource.clip.frequency;
                OnBeat = true;
            }
            else if (mySource.timeSamples > nextEighthSample)
            {
                PlayEighthBeat();
                OnBeat = false;
            }
            else
            {
                OnBeat = false;
            }
        }
        else
        {
            mySource.Play();
            nextFourthSample = delay;
        }

    }
}

