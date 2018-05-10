﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : BeatActor {

    bool isMoving = false;

    public int beatsDuration;
    //public float travelAmount;
    public List<Vector2> positionsList;
    //public List<Vector2> directionsList;

    private Vector2 direction;

    private Vector2 vDir;
    private float scalar;
    private Vector2 destination;
    private int destIdx;
    private float speed, movingTime, moveTimer;
	void Start () {
        SetBehavior();

        moveTimer = movingTime;

        positionsList.Insert(0, (Vector2)transform.position);
        destination = positionsList[1];
        destIdx = 1;

        scalar = transform.position.x;

        direction = destination - (Vector2)transform.position;
        vDir = direction / scalar;

        speed = SetSpeed(out movingTime);
        //Debug.Log("Speed: " + speed);
    }

    void Update () {
        //Debug.Log(isMoving);
       // Debug.Log(destination);
        if(BeatListener() && !isMoving)
        {
            //Debug.Log("Beat Listener and not moving");
            isMoving = true;
            //LoopSound();
            Move();

        }

        if (isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        moveTimer -= Time.deltaTime;

        //transform.position += ((Vector3)direction * travelAmount) * speed * Time.deltaTime;
        transform.position += (Vector3)vDir * speed * Time.deltaTime;
        //Debug.Log("Add position: " + ((Vector3)direction * travelAmount) * Mathf.Abs(speed) * Time.deltaTime);

        if /*(moveTimer <= 0)*/(Vector2.SqrMagnitude((Vector2)transform.position - destination) <= 0.05f)
        {

            //Debug.Log("Arrived");
            moveTimer = movingTime;
            isMoving = false;
            StopSound();

            transform.position = destination;
            //Debug.Log("Count: " + positionsList.Count);
            //Debug.Log("Index: "+destIdx);

            if (positionsList.Count - 1 > destIdx)
            {
               // Debug.Log("SUM");
                destIdx++;
            }
            else
            {
               // Debug.Log("ZERO");
                destIdx = 0;
            }

            destination = positionsList[destIdx];

            direction = destination - (Vector2)transform.position;
            vDir = direction / scalar;

           /* speed = SetSpeed(out movingTime);
            Debug.Log("Speed: " + speed);*/

        }
    }

    private float SetSpeed(out float time)
    {
        float division = 0;

        switch (beatType)
        {
            case BeatManager.BeatType.DownBeat:
                division = 4;
                break;
            case BeatManager.BeatType.FourthBeat:
                division = 4;

                break;
            case BeatManager.BeatType.EighthBeat:
                division = 8;

                break;
            case BeatManager.BeatType.SixteenthBeat:
                division = 16;
                break;
        }

        time = BeatManager.barDuration / division * beatsDuration;

        return Mathf.Abs((direction.x / time) + (direction.y / time));

    }
}
