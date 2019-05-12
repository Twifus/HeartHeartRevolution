using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scores : MonoBehaviour {
    public Players players;

	void Update () {
        string disp = "";
        for (int i = 0; i < players.GetComponent<Menu>().GetNumberPlayers(); i++)
        {
            disp += "J" + (i+1) + " : " + players.getScores()[i] + "\n";
        }
        GetComponent<Text>().text = disp;
    }
}
