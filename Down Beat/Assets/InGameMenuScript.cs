using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuScript : MonoBehaviour
{
    public GameObject MenuPanel;

	// Use this for initialization
	void Start ()
    {
        MenuPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuPanel.activeSelf)
            {
                MenuPanel.SetActive(false);
            }
            else
            {
                MenuPanel.SetActive(true);
            }
        }
		
	}
}
