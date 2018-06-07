using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockScript : BeatActor
{
    public float fallSpeed = 1;
    private Vector3 originalPos;
    public Rigidbody2D myRb;

    private Vector2 firstPosition;
    private float listLength;
    public GameObject rockPrefab;
    public int initialWaitBars = 1;
    public List<individualRockScript> rocksList;
    public ScreenScript myScreen;
    private bool canFall;

    private int currentBar;
    private int currentRock = 0;


    // Use this for initialization
    void Start()
    {
        SetBehavior();

        myScreen = GetComponentInParent<ScreenScript>();
        Vector3 rockScale = rockPrefab.transform.localScale;
        rockScale.y = transform.parent.localScale.y + myScreen.myScreenSettings.sizeScreen * 2;
        rockPrefab.transform.localScale = rockScale;
        firstPosition.x = transform.parent.position.x - myScreen.myScreenSettings.sizeScreen * 1.8f + 0.5f;
        //firstPosition.y = transform.parent.position.y + myScreen.myScreenSettings.sizeScreen;
        firstPosition.y = transform.parent.position.y + myScreen.myScreenSettings.sizeScreen *2;

        listLength = myScreen.myScreenSettings.sizeScreen * 1.8f * 2;
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

            if (currentBar >= initialWaitBars)
            {
                canFall = true;
                //Debug.Log("GO ROCKS");
            }

            return;
        }
        if (BeatListener())
        {
            DropRocks();
            //Debug.Log("Drop Rock");
        }

        if (GameManagerScript.player.actualPlayerState == PlayerScript.PlayerStates.dead)
        {
            ResetRocks(); 
        }

    }
    

    private void DropRocks()
    {
        //rocksList[currentRock].Drop(fallSpeed);
        rocksList[currentRock].Drop(fallSpeed, myScreen.myScreenSettings.positionScreen.y);
        currentRock++;
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
