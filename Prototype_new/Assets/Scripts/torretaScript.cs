using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torretaScript : MonoBehaviour {

	public GameObject b;
    public enum ShootDirection { left,right,up,down}
	public bool left, right, up, down;
	public List<int> eighthBeatsList;

    private bool severalShots;
    private int singleShotBeat;
	private GameObject bl, br, bu, bd;
    private AudioSource beatSource;
    private bool shot = false;

	void Start () 
	{
        if (eighthBeatsList.Count > 1)
        {
            severalShots = true; 
        }
        else
        {
            singleShotBeat = eighthBeatsList[0];
        }
	}
	
	void Update () 
	{
        if (BeatManager.OnEighthBeat)
        {
            if (severalShots)
            {
                foreach (int i in eighthBeatsList)
                {
                    if (BeatManager.eighthNotesCounter == i && !shot)
                    {
                        Shoot();
                        break;
                    }
                }
            }
            else if(BeatManager.eighthNotesCounter == singleShotBeat)
            {
                Shoot();
            }
        }
        else if (!BeatManager.OnEighthBeat && shot)
        {
            shot = false;
        }
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
