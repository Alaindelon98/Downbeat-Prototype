using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatActor : MonoBehaviour
{

    private AudioSource mySource;

    public BeatManager.BeatType beatType;
    public List<int> beatList;
    public AudioClip actSound;
    public float offset;
    protected bool severalBeats;
    protected int singleBeat;
    protected bool actOnBeat = true;


    protected void SetBehavior()
    {
        mySource = GetComponent<AudioSource>();
        if (mySource)
        {
            if (beatType == BeatManager.BeatType.FourthBeat)
            {
                mySource.pitch = 2;
            }
            else if (beatType == BeatManager.BeatType.EighthBeat)
            {
                mySource.pitch = 4;
            }
            else if (beatType == BeatManager.BeatType.SixteenthBeat)
            {
                mySource.pitch = 8;
            }
        }

        if (beatList.Count > 0)
        {
            if (beatList.Count > 1)
            {
                severalBeats = true;
            }
            else
            {
                singleBeat = beatList[0];
            }
        }
        else
        {
            actOnBeat = false;
        }
    }

    protected void PlaySound()
    {
        mySource.PlayOneShot(actSound);
    }


    protected bool BeatListener()
    {
        if (BeatManager.currentBeat != BeatManager.BeatType.NoBeat)
        {
            if (severalBeats)
            {
                foreach (int i in beatList)
                {
                    if (CheckBeat(i))
                    {
                        return true;
                    }
                }
            }
            else if (CheckBeat(singleBeat))
            {
                return true;
            }
        }

        return false;
    }

    protected bool CheckBeat(int i)
    {
        if (beatType == BeatManager.BeatType.FourthBeat && BeatManager.currentBeat == BeatManager.BeatType.FourthBeat)
        {
            if (BeatManager.fourthNotesCounter == i)
            {
                return true;
            }
        }

        else if (beatType == BeatManager.BeatType.EighthBeat && (BeatManager.currentBeat == BeatManager.BeatType.EighthBeat || BeatManager.currentBeat == BeatManager.BeatType.FourthBeat))
        {
            if (BeatManager.eighthNotesCounter == i)
            {
                return true;
            }
        }

        else if (beatType == BeatManager.BeatType.SixteenthBeat && (BeatManager.currentBeat == BeatManager.BeatType.SixteenthBeat || BeatManager.currentBeat == BeatManager.BeatType.EighthBeat || BeatManager.currentBeat == BeatManager.BeatType.FourthBeat))
        {
            if (BeatManager.sixteenthNotesCounter == i)
            {
                return true;
            }
        }

        return false;
    }
}
