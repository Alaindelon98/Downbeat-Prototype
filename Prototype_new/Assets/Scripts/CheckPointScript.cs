using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public ScreenScript myScreen;
    public SpriteRenderer myrenderer;
    public bool checkPointActive;

	// Use this for initialization
	void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (checkPointActive)
        {
        }
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player"&& !checkPointActive)
        {
            
            checkPointActive = true;
            GameManagerScript.playerCheckPoint = this;
            ChangeSprite();
        }
        
      

    }
    public void ChangeSprite(bool state)
    {

    }
}
