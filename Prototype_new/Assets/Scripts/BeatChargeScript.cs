using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatChargeScript : MonoBehaviour {

    private Vector3 Originalscale,maxScale;
    public Transform maxScaleObject;
    public float shakeAmount,shakeTime;
   
    private bool isShaking;
    // Use this for initialization
	void Start ()
    {
        Originalscale = transform.localScale;
        maxScale = maxScaleObject.localScale;
        isShaking = false;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (BeatManager.OnBeat)
        {
            switch (BeatManager.fourthNotesCounter)
            {
                case 1:
                    transform.localScale = maxScale;
                    StartCoroutine("ShakeFunction");
                    break;
                case 2:
                    transform.localScale = maxScale/4;
                    break;
                case 3:
                    transform.localScale =  maxScale / 4*2;
                    break;
                case 4:
                    transform.localScale = maxScale / 4*3;
                    
                    break;


            }
           
        }
      
        

        //if(BeatManager.fourthNotesCounter == 1 && BeatManager.OnBeat)
        //{
        //    transform.localScale = Originalscale;
        //    StartCoroutine("ShakeFunction");
        //}
        //else if(BeatManager.OnBeat)
        //{
        //    float nextScale = maxScale.sqrMagnitude / 4 - BeatManager.fourthNotesCounter;
        //    transform.localScale = new Vector3(nextScale,nextScale,nextScale);
        //}


        if (isShaking)
        {
            Vector3 newPos = maxScaleObject.position + (Random.insideUnitSphere * (Time.deltaTime * shakeAmount));
            newPos.z = maxScaleObject.position.z;
            newPos.y = maxScaleObject.position.y;
            maxScaleObject.position = newPos;
        }

      
        
        
    }
    IEnumerator ShakeFunction()
    {
        Debug.Log("Hello");
        Vector3 originalPos = maxScaleObject.position;
        if (!isShaking)
        {
            isShaking = true;
        }

        yield return new WaitForSeconds(shakeTime);

        maxScaleObject.position = originalPos;
        isShaking = false;
    }

    
}
