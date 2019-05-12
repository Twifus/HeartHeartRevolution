using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour {
    private int play = 0;
    private int lat = 5;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
	
	void FixedUpdate () {
        play = (play + 1) % (lat * 4);
        if (play == 0)
        {
            Heart2.SetActive(false);
            Heart1.SetActive(true);
        }
        else if (play == lat)
        {
            Heart1.SetActive(false);
            Heart2.SetActive(true);
        }
        else if (play == lat * 2)
        {
            Heart2.SetActive(false);
            Heart3.SetActive(true);
        }
        else if (play == lat * 3)
        {
            Heart3.SetActive(false);
            Heart2.SetActive(true);
        }
    }
}
