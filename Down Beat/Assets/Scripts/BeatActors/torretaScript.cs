using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class torretaScript : BeatActor {

	public GameObject b;
    public enum ShootDirection { left,right,up,down}
	public bool left, right, up, down;
    public float offset;

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
		bl.transform.position = new Vector3 (this.transform.position.x-offset, this.transform.position.y, this.transform.position.z);
		bl.transform.Rotate (0,0,90);
		bl.GetComponent<bulletScript> ().directionH = -1;
		bl.GetComponent<bulletScript> ().directionV = 0;
	}
	public void ShootingRight ()
	{
		br = Instantiate (b);
		br.transform.position = new Vector3 (this.transform.position.x+offset, this.transform.position.y, this.transform.position.z);
		br.transform.Rotate (0,0,-90);
		br.GetComponent<bulletScript> ().directionH = 1;
		br.GetComponent<bulletScript> ().directionV = 0;
	}
	public void ShootingUp ()
	{
		bu = Instantiate (b);
		bu.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y+offset, this.transform.position.z);
		bu.GetComponent<bulletScript> ().directionH = 0;
		bu.GetComponent<bulletScript> ().directionV = 1;

	}
	public void ShootingDown ()
	{
		bd = Instantiate (b);
		bd.transform.position =  new Vector3 (this.transform.position.x, this.transform.position.y-offset, this.transform.position.z);
		bd.GetComponent<bulletScript> ().FlipY ();
		bd.GetComponent<bulletScript> ().directionH = 0;
		bd.GetComponent<bulletScript> ().directionV = -1;
	}
}
