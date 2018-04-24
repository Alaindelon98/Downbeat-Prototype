using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{

    public float bpm;
    public int beatUnit = 4;
    public int barLength = 4;
    public int strongBeat = 1;
    private float bps;
    AudioSource mySource;
    public AudioClip downBeat, offBeat;

    public static int fourthNotesCounter;
    public static int eighthNotesCounter;
    public static bool OnBeat, OnEighthBeat;

    public int delay;
    public float loopSampleRange;
    private float firstBeatRange;

    float nextFourthSample, nextEighthSample;

    bool nextBeatIsOut;

    void Start()
    {
        mySource = GetComponent<AudioSource>();
        fourthNotesCounter = barLength;
        eighthNotesCounter = barLength * 2;
        bps = bpm / 60;

        nextFourthSample = delay * mySource.clip.frequency;
        nextEighthSample = delay * mySource.clip.frequency;
        OnBeat = false;
        OnEighthBeat = false;

        firstBeatRange = (delay * mySource.clip.frequency) + (1 / bps * mySource.clip.frequency);
    }

    void Update()
    {
        if (mySource.isPlaying)
        {
            if (nextBeatIsOut && mySource.timeSamples < firstBeatRange)
            {
                nextFourthSample = delay * mySource.clip.frequency;
                nextEighthSample = delay * mySource.clip.frequency;


                nextBeatIsOut = false;
            }
            if (mySource.timeSamples > nextFourthSample)
            {
                PlayFourthBeat();
                PlayEighthBeat();
                
                OnBeat = true;
                OnEighthBeat = true;
            }
            else if (mySource.timeSamples > nextEighthSample)
            {
                PlayEighthBeat();
                OnBeat = false;
                OnEighthBeat = true;

            }
            else
            {
                OnBeat = false;
                OnEighthBeat = false;

            }

            if (Mathf.Abs(nextFourthSample - mySource.clip.samples) < loopSampleRange && !nextBeatIsOut)
            {
                nextBeatIsOut = true;
            }

            if (Mathf.Abs(nextEighthSample - mySource.clip.samples) < loopSampleRange && !nextBeatIsOut)
            {
                nextBeatIsOut = true;

            }
        }

    }

    void PlayFourthBeat()
    {

        nextFourthSample += 1 / bps * mySource.clip.frequency;


        if (fourthNotesCounter == barLength)
        {
            fourthNotesCounter = 1;
        }
        else
        {
            fourthNotesCounter++;
        }

        if (fourthNotesCounter == 1)
        {
            mySource.PlayOneShot(downBeat);
        }

        else
        {
            mySource.PlayOneShot(offBeat);
        }

        /*switch (fourthNotesCounter)
        {
            case 1:

                mySource.PlayOneShot(downBeat);

                break;

        }*/


    }
    void PlayEighthBeat()
    {
        nextEighthSample += 0.5f / bps * mySource.clip.frequency;

        //mySource.PlayOneShot(offBeat);

        if (eighthNotesCounter == barLength*2)
        {
            eighthNotesCounter = 1;
        }
        else
        {
            eighthNotesCounter++;
        }

    }

}