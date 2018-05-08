using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : BeatActor {

    enemyScript enemySettings;

    public float enemySpeed, enemyJumpVel;
    public int enemyDirection = 1;

    public float shootStrength;

    public GameObject enemyPrefab;
    void Start () {
        SetBehavior();



       /* enemySettings = enemyPrefab.GetComponent<enemyScript>();
        enemySettings.speed = enemySpeed;
        enemySettings.jumpVelocity = enemyJumpVel;
        enemySettings.direction = enemyDirection;*/
    }
	
	// Update is called once per frame
	void Update () {
        if (!actOnBeat)
        {
            return;
        }

        if (BeatListener())
        {
            SpawnEnemy();
            Debug.Log("Spawn");
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.x += 0.5f * enemyDirection;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        //newEnemy.GetComponent<enemyScript>().GetCopyOf(GetComponent<enemyScript>());

        //enemyScript newEnemyScript = newEnemy.AddComponent<enemyScript>(GetComponent<enemyScript>());

       /* enemyScript newEnemyScript = newEnemy.GetComponent<enemyScript>();
        enemySettings = GetComponent<enemyScript>();
        newEnemyScript.GetCopyOf(enemySettings);*/
        Rigidbody2D rb = newEnemy.GetComponent<Rigidbody2D>();
        //newEnemy.GetComponent<enemyScript>().myRigidbody = rb;


        rb.AddForce(new Vector2(enemyDirection * shootStrength, 0));
    }
}
