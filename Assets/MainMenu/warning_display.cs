using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warning_display : MonoBehaviour {

    public Menu menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!menu.IsReadyToPlay() && menu.GetNumberPlayers() != 0)
        {
            gameObject.GetComponent<Text>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Text>().enabled = false;
        }
    }
}
