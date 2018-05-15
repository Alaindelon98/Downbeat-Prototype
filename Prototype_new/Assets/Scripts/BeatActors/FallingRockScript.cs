using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : BeatActor
{
    public float initialFallSpeeed = 1, shakeAmt;
    public int BeatsToFall, BeatCount;
    private bool falling, shaking;
    private Vector3 originalPos;
    public Rigidbody2D myRb;

    public Vector2 firstPosition;
    public float listLength;
    public BeatManager.BeatType beatInterval;

    private GameObject debrisList;



    // Use this for initialization
    void Start()
    {

        falling = false;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (!falling)
        {
            if (shaking)
            {
                Vector3 newPos = originalPos + (Random.insideUnitSphere * (Time.deltaTime * shakeAmt));
                newPos.y = transform.position.y;
                newPos.z = transform.position.z;
                transform.position = newPos;
            }
        }
        else
        {
            if (myRb.velocity.y <= 0)
            {
                falling = false;
            }
        }*/
    }
    /*public void SetToFall()
    {
        transform.position = originalPos;
        falling = true;
        myRb.isKinematic = false;
        myRb.velocity = Vector2.down * initialFallSpeeed;

    }
    public void SumBeats()
    {
        BeatCount++;

        if (BeatCount == BeatsToFall)
        {
            shaking = false;
            SetToFall();
        }
        if (BeatCount == (BeatsToFall - 1))
        {
            SetToShake();
        }
    }
    public void SetToShake()
    {
        shaking = true;
        originalPos = transform.position;

    }*/
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && falling)
        {
            if (myRb.velocity.y > 0)
            {
                GameManagerScript.player.ChangePlayerState(PlayerScript.PlayerStates.dying);
            }
        }
    }
}
