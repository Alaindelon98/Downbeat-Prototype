using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatActor : MonoBehaviour
{

    private AudioSource mySource;

    public BeatManager.BeatType beatType;
    public List<int> beatList;
    public AudioClip actSound;
    protected bool severalBeats;
    protected int singleBeat;
    protected bool actOnBeat = true;
    

    protected void SetBehavior()
    {
        mySource = GetComponent<AudioSource>();
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
                    if(CheckBeat(i))
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

public class torretaScript : BeatActor {

	public GameObject b;
    public enum ShootDirection { left,right,up,down}
	public bool left, right, up, down;

	private GameObject bl, br, bu, bd;
    private bool shot = false;

	void Start () 
	{
        SetBehavior();
	}
	
	void Update () 
	{
        if(!actOnBeat)
        {
            return;
        }

        if(BeatListener() && !shot)
        {
            Shoot();
            shot = true;
        }
        
        if (shot && BeatManager.currentBeat == BeatManager.BeatType.NoBeat)
            shot = false;
    }

    void Shoot()
    {
        if (left)
        {
            Shootingleft();
        }
        if (right)
        {
            ShootingRight();
        }
        if (up)
        {
            ShootingUp();
        }
        if (down)
        {
            ShootingDown();
        }
        shot = true;

        PlaySound();

        //if(beatType == BeatManager.BeatType.SixteenthBeat)
        //Debug.Log("Beat: "+ BeatManager.sixteenthNotesCounter);
    }


    public void Shootingleft ()
	{
		bl = Instantiate (b);
		bl.transform.position = this.transform.position;
		bl.GetComponent<bulletScript> ().directionH = -1;
		bl.GetComponent<bulletScript> ().directionV = 0;
	}
	public void ShootingRight ()
	{
		br = Instantiate (b);
		br.transform.position = this.transform.position;
		br.GetComponent<bulletScript> ().directionH = 1;
		br.GetComponent<bulletScript> ().directionV = 0;
	}
	public void ShootingUp ()
	{
		bu = Instantiate (b);
		bu.transform.position = this.transform.position;
		bu.transform.Rotate (0,0,90);
		bu.GetComponent<bulletScript> ().directionH = 0;
		bu.GetComponent<bulletScript> ().directionV = 1;

	}
	public void ShootingDown ()
	{
		bd = Instantiate (b);
		bd.transform.position = this.transform.position;
		bd.transform.Rotate (0,0,-90);
		bd.GetComponent<bulletScript> ().directionH = 0;
		bd.GetComponent<bulletScript> ().directionV = -1;
	}
}
