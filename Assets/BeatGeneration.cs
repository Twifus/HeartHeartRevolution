using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGeneration : MonoBehaviour {

    public Sprite beatSprite;
    public static List<GameObject> livingBeats;

	// Use this for initialization
	void Start () {
        livingBeats = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        float randf = Random.Range(0f, 10f);
        if (randf > 9)
        {
            GameObject newBeat = new GameObject();
            newBeat.AddComponent<SpriteRenderer>();
            newBeat.GetComponent<SpriteRenderer>().sprite = beatSprite;
            newBeat.AddComponent<PolygonCollider2D>();
            newBeat.AddComponent<Rigidbody2D>();
            newBeat.GetComponent<Rigidbody2D>().mass = 0;
            newBeat.AddComponent<BeatDirectioning>();
            newBeat.AddComponent<DestroyNoteOnColide>();
            newBeat.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            livingBeats.Add(newBeat);
        }
    }
}
