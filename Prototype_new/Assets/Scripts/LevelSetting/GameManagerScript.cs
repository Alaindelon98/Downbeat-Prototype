using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {


    public static ScreenScript actualScreen;
    public float respawnTime;
    public static PlayerScript player;
    public int sceneNumber;
    public static int currentScene;
 

	// Use this for initialization
	void Start ()
    {
        //DontDestroyOnLoad(this.gameObject);

        currentScene = sceneNumber;
	}
    void Update()
    {
		
    }

    public static void LevelSetup(ScreenScript _actualScreen, PlayerScript _player, CheckPointScript _originalCheckPoint)
    {
        actualScreen = _actualScreen;
        player = _player;
        player.playerCheckPoint = _originalCheckPoint;
    }
	// Update is called once per frame

    
    public static void ChangeCheckPoint(CheckPointScript newcheckPoint)
    {
      player.playerCheckPoint.checkPointActive = false;
      player.playerCheckPoint.ChangeSprite(false);
      player.playerCheckPoint = newcheckPoint;
       
    }
}
