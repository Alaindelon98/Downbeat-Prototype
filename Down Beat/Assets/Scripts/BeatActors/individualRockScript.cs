using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class individualRockScript : MonoBehaviour {

    private bool isFalling;
    private bool fallen;
    public float fallSpeed = 1;
    //public Texture2D tex;

    private BoxCollider2D col;
    private float endPos;
    private Vector3 startPos;
    public GameObject lavaPrefab;

    public FallingRockScript rockManager;

    private bool killedPlayer, copyFalling;
    private GameObject myCopy;
	void Start () {
        col = GetComponent<BoxCollider2D>();

        //SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        //sr.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 5.0f);

        startPos = transform.position;
    }
	
	void Update () {
        if (!fallen)
        {
            if (isFalling)
            {
                //Vector2 offset = col.offset;
                //offset.y -= fallSpeed * Time.deltaTime;
                //col.offset = offset;
                Vector3 newPos = transform.position;
                newPos.y -= fallSpeed * Time.deltaTime;
                transform.position = newPos;
            }

            //if (col.offset.y <= -0.5f)
            if (transform.position.y <= endPos)
            {
                if (!copyFalling)
                {
                    //col.offset = new Vector2(0, -0.5f) ;
                    Vector3 finalPos = transform.position;
                    finalPos.y = endPos;
                    transform.position = finalPos;
                    isFalling = false;
                    fallen = true;
                    Debug.Log("Fallen");
                    myCopy = Instantiate(lavaPrefab, transform.position, transform.rotation);
                    copyFalling = true;
                    myCopy.transform.parent = this.transform;
                    myCopy.GetComponent<individualRockScript>().enabled = false;
                }
            }
        }

        else
        { 
            if (copyFalling)
            {
                Debug.Log("FallingCopy");
                Vector3 newCopyPos = transform.position;
                newCopyPos.y -= fallSpeed * Time.deltaTime;
                myCopy.transform.position = newCopyPos;
                Debug.Log(myCopy.transform.position);

                if (myCopy.transform.position.y <= (endPos - (endPos - startPos.y)))
                {
                    Debug.Log("don't fall");
                    copyFalling = false;
                }
            }
        }
	}

    public void Drop(float _fallSpeed, float _endPos)
    {
        isFalling = true;
        fallSpeed = _fallSpeed;
        endPos = _endPos;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && isFalling && !killedPlayer)
        {
            Debug.Log("Die");
                
           GameManagerScript.player.ChangePlayerState(PlayerScript.PlayerStates.dying);
            killedPlayer = true;
        }
    }

    public void RestoreCollider()
    {
        //col.offset = Vector2.up;
        transform.position = startPos;
        killedPlayer = false;
        isFalling = false;
        fallen = false;

        Destroy(myCopy);

    }
}
