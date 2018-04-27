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

    public enum BeatType
    {
        NoBeat,
        DownBeat,
        FourthBeat,
        EighthBeat,
        SixteenthBeat
    }

    public static int fourthNotesCounter;
    public static int eighthNotesCounter;
    public static int sixteenthNotesCounter;
    public static bool OnBeat, OnEighthBeat, OnSixteenthBeat;
    public static BeatType currentBeat;

    public int delay;
    public float loopSampleRange;
    private float firstBeatRange;

    float nextFourthSample, nextEighthSample, nextSixteenthSample;

    bool nextBeatIsOut;


    void Start()
    {
        mySource = GetComponent<AudioSource>();
        fourthNotesCounter = barLength;
        eighthNotesCounter = barLength * 2;
        sixteenthNotesCounter = barLength * 4;
        bps = bpm / 60;

        nextFourthSample = delay * mySource.clip.frequency;
        nextEighthSample = delay * mySource.clip.frequency;
        nextSixteenthSample = delay * mySource.clip.frequency;

        OnBeat = false;
        OnEighthBeat = false;
        OnSixteenthBeat = false;
        currentBeat = BeatType.NoBeat;

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
                nextSixteenthSample = delay * mySource.clip.frequency;


                nextBeatIsOut = false;
            }
            if (mySource.timeSamples > nextFourthSample)
            {
                CountFourthBeat();
                CountEighthBeat();
                CountSixteenthBeat();
                
                OnBeat = true;
                OnEighthBeat = true;
                OnSixteenthBeat = true;

                currentBeat = BeatType.FourthBeat;

                if (fourthNotesCounter == 1)
                {
                    currentBeat = BeatType.DownBeat;
                    mySource.PlayOneShot(downBeat);
                }
            }
            else if (mySource.timeSamples > nextEighthSample)
            {
                CountEighthBeat();
                CountSixteenthBeat();

                currentBeat = BeatType.EighthBeat;

                OnBeat = false;
                OnEighthBeat = true;
                OnSixteenthBeat = true;

            }

            else if (mySource.timeSamples > nextSixteenthSample)
            {
                CountSixteenthBeat();

                currentBeat = BeatType.SixteenthBeat;

                OnSixteenthBeat = true;
                OnBeat = false;
                OnEighthBeat = false;

            }
            else
            {

                currentBeat = BeatType.NoBeat;
                OnBeat = false;
                OnEighthBeat = false;
                OnSixteenthBeat = false;
            }

            if (Mathf.Abs(nextFourthSample - mySource.clip.samples) < loopSampleRange && !nextBeatIsOut)
            {
                nextBeatIsOut = true;
            }
        }

        //if(currentBeat == BeatType.DownBeat)
        //Debug.Log(currentBeat);
        //Debug.Log(mySource.timeSamples);
        //Debug.Log(nextBeatIsOut);
        //Debug.Log("NextFourth:"+nextFourthSample);
        //Debug.Log("All samples: " + mySource.clip.samples);
    }

    void CountFourthBeat()
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

        

        /*else
        {
            mySource.PlayOneShot(offBeat);
        }*/
    }
    void CountEighthBeat()
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

    void CountSixteenthBeat()
    {
        nextSixteenthSample += 0.25f / bps * mySource.clip.frequency;

        //mySource.PlayOneShot(offBeat);

        if (sixteenthNotesCounter == barLength * 4)
        {
            sixteenthNotesCounter = 1;
        }
        else
        {
            sixteenthNotesCounter++;
        }


    }

}