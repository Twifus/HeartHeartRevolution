using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class display_connected : MonoBehaviour {

    public Menu menu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        int nPlayers = menu.GetNumberPlayers();

        if (nPlayers == 0)
        {
            gameObject.GetComponent<Text>().text = "No player connected";
        }
        else
        {
            gameObject.GetComponent<Text>().text = nPlayers + " players connected";
        }
    }
}
