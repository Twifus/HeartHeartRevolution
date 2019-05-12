using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDirectioning : MonoBehaviour {

    //we want the speed to be fast enough for the beat to appear  5 second before it should be played
    private float speed;
    Vector2 startPosition;
    Vector2 endPosition;
    GameObject start;
    GameObject end;
    Vector2 direction;
    private float dateOfBirth;


	// Use this for initialization
	void Start () {
        dateOfBirth = Time.time;
        start = GameObject.Find("rightFrontier");
        end = GameObject.Find("leftFrontier");
                
        float randomOffset = Random.Range(-start.GetComponent<RectTransform>().rect.height / 2.5f,
                                            start.GetComponent<RectTransform>().rect.height / 2.5f);
        startPosition = start.transform.position;
        startPosition.y += randomOffset;
        endPosition = end.transform.position;
        endPosition.y += randomOffset;
        direction = endPosition - startPosition;
        transform.position = startPosition;

    }

    // Update is called once per frame
    void Update () {
        float proportion = 1 - (dateOfBirth + 5f - Time.time) / 5f;
        this.transform.position = startPosition + proportion * direction;
	}
}
