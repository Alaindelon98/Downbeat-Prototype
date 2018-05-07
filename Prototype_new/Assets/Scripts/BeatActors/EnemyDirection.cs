using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour
{

    public enemyScript myEnemy;

	// Use this for initialization
	
    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("COLLISION");
        //Debug.Log(myEnemy.direction);
        myEnemy.direction *= -1;
    }
}
