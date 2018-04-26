using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour
{

    public enemyScript myEnemy;

	// Use this for initialization
	void Start ()
    {

		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        myEnemy.direction *= -1;
    }
}
