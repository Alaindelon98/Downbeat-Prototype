using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torretaScript : MonoBehaviour {

	public GameObject b;
	public bool left, right, up, down;
	public float maxCounter;
	private float counter;
	private GameObject bl, br, bu, bd;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		counter += Time.deltaTime;
		if (counter >= maxCounter) {
			if (left) {
				Shootingleft ();
			}
			if (right) {
				ShootingRight ();
			}
			if (up) {
				ShootingUp ();
			}
			if (down) {
				ShootingDown ();
			}
			counter = 0;
		}
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
