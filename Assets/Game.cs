using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Audio;

public class Game : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private List<float> beatsValue = new List<float>(512);
    private List<float> beatsTime = new List<float>(512);
    private float[] bands;

    // Use this for initialization
    void Start()
    {
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onBeat.AddListener(onOnbeatDetected);
        processor.onSpectrum.AddListener(onSpectrum);
    }

    // Update is called once per frame
    /*void Update()
    {
        foreach (float f in beatBehaviour.getData())
        {
            lineRenderer.SetPosition(lineRenderer.positionCount++, new Vector3(f, 1.0f));
            lineRenderer.SetPosition(lineRenderer.positionCount++, new Vector3(f+0.5f, 0.0f));
            beats.Add(f);
        }

        beatBehaviour.PurgeData();

        /*if (audio.clip.length <= audio.time)
        {
            lineRenderer.positionCount = beats.ToArray().Length;
            int i = 0;
            foreach (float f in beats)
            {
                Debug.Log(f);
                lineRenderer.SetPosition(i++, new Vector3(f, 1.0f));
                lineRenderer.SetPosition(i++, new Vector3(f + 0.1f, 0.0f));
            }
        }
    }*/

    void onOnbeatDetected()
    {
        AudioSource audio = GetComponent<AudioSource>();
        //Debug.Log("Beat:" + audio.time);
        beatsTime.Add(audio.time);
        beatsValue.Add(bands.Max() * 100);
    }

    //This event will be called every frame while music is playing
    void onSpectrum(float[] spectrum)
    {
        bands = spectrum;
    }

    public List<float> getBeatsTime()
    {
        return beatsTime;
    }

    public List<float> getBeatsValue()
    {
        return beatsValue;
    }
}
