using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNoteOnColide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Triggered");
        Debug.Log("aaaa "+BeatGeneration.livingBeats.Count);
        BeatGeneration.livingBeats.Remove(gameObject);
        Debug.Log("bbbb "+BeatGeneration.livingBeats.Count);
        Destroy(gameObject);
    }
}
