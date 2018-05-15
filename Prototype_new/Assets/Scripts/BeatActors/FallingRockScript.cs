using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : BeatActor
{
    public float fallSpeed = 1;//, shakeAmt;
    //public int BeatsToFall, BeatCount;
    //private bool falling, shaking;
    private Vector3 originalPos;
    public Rigidbody2D myRb;

    public Vector2 firstPosition;
    public float listLength;
    //public BeatManager.BeatType beatInterval;
    public GameObject rockPrefab;
    public int initialWaitBars = 1;
    public List<individualRockScript> rocksList;
    private bool canFall;

    private int currentBar;
    private int currentRock = 0;


    // Use this for initialization
    void Start()
    {
        SetBehavior();

        rocksList = new List<individualRockScript>();
        //falling = false;

        for (int i = 0; i < listLength; i++)
        {
            GameObject rock = Instantiate(rockPrefab, rockPrefab.transform.position, Quaternion.identity); 
            if (i == 0)
            {
                rock.transform.position = firstPosition; 
            } 
            else
            {
                Vector2 rockPosition = rock.transform.position = rocksList[i - 1].transform.position;
                rockPosition.x += 1;
                rock.transform.position = rockPosition;

            }

            rock.transform.parent = transform;
            individualRockScript rockScript = rock.GetComponent<individualRockScript>();
            rockScript.rockManager = this;
            rocksList.Add(rockScript);
            //Instantiate(rock, rock.transform.position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!canFall)
        {
            if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat)
            {
                currentBar += 1;
            }

            if (currentBar == initialWaitBars)
            {
                canFall = true;
            }

            return;
        }
        if (BeatListener())
        {
            DropRocks();
            //Debug.Log("Drop Rock");
        }
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

    private void DropRocks()
    {
        rocksList[currentRock].Drop(fallSpeed);
        currentRock++;
        //rocksList.RemoveAt(0);
    }

    public void ResetRocks()
    {
        currentRock = 0;
        canFall = false;
        currentBar = 0;
        foreach (individualRockScript r in rocksList)
        {
            r.RestoreCollider();
            
        }
    }

}
