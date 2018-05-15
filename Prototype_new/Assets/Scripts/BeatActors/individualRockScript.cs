using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class individualRockScript : MonoBehaviour {

    private bool isFalling;
    private bool fallen;
    public float fallSpeed = 1;

    private BoxCollider2D col;

    public FallingRockScript rockManager;

    private bool killedPlayer;

	void Start () {
        col = GetComponent<BoxCollider2D>();
	}
	
	void Update () {
        if (!fallen)
        {
            if (isFalling)
            {
                Vector2 offset = col.offset;
                offset.y -= fallSpeed * Time.deltaTime;
                col.offset = offset;
            }

            if (col.offset.y <= 0)
            {
                col.offset = Vector2.zero;
                isFalling = false;
                fallen = true;
            }
        }
	}

    public void Drop(float speed)
    {
        isFalling = true;
        fallSpeed = speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && isFalling && !killedPlayer)
        {
           GameManagerScript.player.ChangePlayerState(PlayerScript.PlayerStates.dying);
            killedPlayer = true;
            rockManager.ResetRocks();
        }
    }

    public void RestoreCollider()
    {
        col.offset = Vector2.up;
        killedPlayer = false;
        isFalling = false;
        fallen = false;

    }
}
