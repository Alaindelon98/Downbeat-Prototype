using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {


    public static ScreenScript actualScreen;
    public float respawnTime;
    public static PlayerScript player;
    public static CheckPointScript playerCheckPoint;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
	}
    void Update()
    {

    }

    public static void LevelSetup(ScreenScript _actualScreen, PlayerScript _player, CheckPointScript _originalCheckPoint)
    {
        actualScreen = _actualScreen;
        player = _player;
        playerCheckPoint = _originalCheckPoint;
    }
	// Update is called once per frame

    public static void PlayerDeath()
    {
        if (!(actualScreen ==playerCheckPoint.myScreen))
        {
            playerCheckPoint.myScreen.ChangeScreen(playerCheckPoint.myScreen);

        }
        player.rb.velocity = Vector3.zero;
        player.transform.position = playerCheckPoint.transform.position;
        //player.gameObject.SetActive(true);
    }
    public static void ChangeCheckPoint(CheckPointScript newcheckPoint)
    {
        playerCheckPoint.checkPointActive = false;
        playerCheckPoint.ChangeSprite(false);
        playerCheckPoint = newcheckPoint;
       
    }
}
