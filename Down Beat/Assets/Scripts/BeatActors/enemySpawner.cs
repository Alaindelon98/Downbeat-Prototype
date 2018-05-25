using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : BeatActor {

    enemyScript enemySettings, spawnerSettings;

    //public float enemySpeed, enemyJumpVel;
    public Vector2 spawnDirection = new Vector2(1, 0);
    public float shootStrength;



    public GameObject enemyPrefab;
    void Start () {
        SetBehavior();



        enemySettings = enemyPrefab.GetComponent<enemyScript>();
        spawnerSettings = GetComponent<enemyScript>();

        enemySettings.speed = spawnerSettings.speed;
        
        enemySettings.direction = spawnerSettings.direction;
        enemySettings.beatType = spawnerSettings.beatType;
        enemySettings.beatList = spawnerSettings.beatList;
        enemySettings.actSound = spawnerSettings.actSound;
        enemySettings.waitBarInterval = spawnerSettings.waitBarInterval;
        
    }

    private void OnDisable()
    {
        
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
            //Debug.Log("Spawn");
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.x += 0.5f * enemySettings.direction;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        Rigidbody2D rb = newEnemy.GetComponent<Rigidbody2D>();


        rb.AddForce(spawnDirection * shootStrength);
    }
}
