using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public ScreenScript myScreen;
    public SpriteRenderer myrenderer;
    public List<Sprite> StateSprites;
    public bool checkPointActive;
    public Animation WiggleAnim;

	// Use this for initialization
	void Start ()
    {
        ChangeSprite(checkPointActive);

        if (checkPointActive)
        {
           
            GameManagerScript.player.playerCheckPoint = this;
        }
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (checkPointActive)
        {
            //if(BeatManager.currentBeat==BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat)
            //{
            //    WiggleAnim.Play();
            //}
        }
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player"&& !checkPointActive)
        {
            
            checkPointActive = true;
            GameManagerScript.ChangeCheckPoint(this);
            ChangeSprite(true);
        }
        
      

    }
    public void ChangeSprite(bool state)
    {
        if (state)
        {
            myrenderer.color = Color.cyan;
           // myrenderer.color = Color.white;
            myrenderer.sprite = StateSprites[0];
        }
        else
        {
            myrenderer.color = Color.gray;
            myrenderer.sprite = StateSprites[1];
        }
    }
}
