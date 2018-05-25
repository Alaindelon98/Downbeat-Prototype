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

    public FallingRockScript rockManager;

    private bool killedPlayer;

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
                //col.offset = new Vector2(0, -0.5f) ;
                Vector3 finalPos = transform.position;
                finalPos.y = endPos;
                transform.position = finalPos;
                isFalling = false;
                fallen = true;
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
            rockManager.ResetRocks();
        }
    }

    public void RestoreCollider()
    {
        //col.offset = Vector2.up;
        transform.position = startPos;
        killedPlayer = false;
        isFalling = false;
        fallen = false;

    }
}
