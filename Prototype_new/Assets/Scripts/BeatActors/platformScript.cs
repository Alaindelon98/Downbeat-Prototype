using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : BeatActor {

    bool isMoving = false;

    public int beatsDuration;
    public float travelAmount;
    public Vector2 direction;

    private Vector2 destination;
    private float speed, movingTime, moveTimer;
	void Start () {
        SetBehavior();

        speed = SetSpeed(out movingTime);
        Debug.Log("Speed: " + speed);

        destination = (Vector2)transform.position + direction * travelAmount;

        moveTimer = movingTime;
    }

    void Update () {
        //Debug.Log(isMoving);
        Debug.Log(destination);
        if(BeatListener() && !isMoving)
        {
            Debug.Log("Beat Listener and not moving");
            isMoving = true;
            LoopSound();
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
        
        transform.position += ((Vector3)direction * travelAmount) * speed * Time.deltaTime;
        //Debug.Log("Add position: " + ((Vector3)direction * travelAmount) * Mathf.Abs(speed) * Time.deltaTime);



        if (moveTimer <= 0)//(Vector2.SqrMagnitude((Vector2)transform.position - destination) <= 0.1f)
        {
            moveTimer = movingTime;
            isMoving = false;
            StopSound();
            direction *= -1;

            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);

            destination = (Vector2)transform.position + (direction * travelAmount);


            //Debug.Log("Change Direction");
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
        
        return (direction.x / time) + (direction.y / time);

    }
}
