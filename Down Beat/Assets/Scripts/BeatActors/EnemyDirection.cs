using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : MonoBehaviour
{

    public enemyScript myEnemy;

	// Use this for initialization
	void Update ()
	{
		
	}
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            //Debug.Log("Go left");
            myEnemy.direction *=-1;
           
            Debug.Log("NOW");
     
        }
        //Debug.Log("COLLISION");
        //Debug.Log(myEnemy.direction);
    }
}
