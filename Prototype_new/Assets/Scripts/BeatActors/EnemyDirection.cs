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
        if (myEnemy.direction == 1)
        {
            //Debug.Log("Go left");
            myEnemy.direction = -1;
        }
        else
        {
            //Debug.Log("Go right");
            myEnemy.direction = 1;
        }
    }
}
