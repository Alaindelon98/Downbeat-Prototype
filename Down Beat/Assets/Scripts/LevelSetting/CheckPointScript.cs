using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public ScreenScript myScreen;
    public SpriteRenderer myrenderer;
    public List<Sprite> StateSprites;
    public bool checkPointActive;
	public Animation WiggleAnim, CheckPointAnim;
	public Animator Anim;

    public GameObject particles;

    public AudioSource CheckPointTake;
	// Use this for initialization
	void Start ()
    {
        //ChangeSprite(checkPointActive);

		CheckPointAnim = GetComponent<Animation>();
		//Anim = GetComponent<Animator>();

        if (checkPointActive)
        {
            ChangeSprite(true);
            particles.SetActive(true);
            GameManagerScript.player.playerCheckPoint = this;
        }
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player"&& !checkPointActive)
        {
            checkPointActive = true;
            GameManagerScript.ChangeCheckPoint(this);
            CheckPointTake.Play();
          
            ChangeSprite(true);
        }
        
      

    }
    public void ChangeSprite(bool state)
    {
        if (state)
        {
            particles.SetActive(true);
            Anim.SetBool ("check", true);
            //myrenderer.color = Color.cyan;
            //myrenderer.sprite = StateSprites[0];
        }
        else
        {
            particles.SetActive(false);
            Anim.SetBool ("check", false);
            //myrenderer.color = Color.gray;
            //myrenderer.sprite = StateSprites[1];
        }
    }
}
