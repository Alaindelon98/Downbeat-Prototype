using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : BeatActor {

    enemyScript enemySettings, spawnerSettings;

    //public float enemySpeed, enemyJumpVel;
    public Vector2 spawnDirection = new Vector2(1, 0);
    public float shootStrength;
	private Rigidbody2D myRigidbody;


    public GameObject enemyPrefab;
    void Start () {
        SetBehavior();
		myRigidbody = GetComponent<Rigidbody2D>();
		myRigidbody.bodyType = RigidbodyType2D.Static;

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
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
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

        newEnemy.transform.parent = transform;

        Rigidbody2D rb = newEnemy.GetComponent<Rigidbody2D>();
		//rb.bodyType = RigidbodyType2D.Dynamic;

        rb.AddForce(spawnDirection * shootStrength);
    }
}
