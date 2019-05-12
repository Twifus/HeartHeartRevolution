using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePlacement : MonoBehaviour {

    Vector3[] Positions;

    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update() {

        int size = BeatGeneration.livingBeats.Count;
        Positions = new Vector3[size];
        for (int i = 0; i < size ; i++)
        {
            Positions[i] = BeatGeneration.livingBeats[i].transform.position;
            Debug.Log(BeatGeneration.livingBeats[i].transform.position);
            Debug.Log(Positions[i]);
        }
        //gameObject.GetComponent<LineRenderer>().enabled = true;
        gameObject.GetComponent<LineRenderer>().positionCount = size;
        gameObject.GetComponent<LineRenderer>().SetPositions(Positions);

        
    }
}
