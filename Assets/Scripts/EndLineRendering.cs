using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLineRendering : MonoBehaviour {

    private Vector2 center;
    private LineRenderer lineRenderer;
    private Vector3 pointUp, pointDown;
    public Material myMaterial;


	// Use this for initialization
	void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        center = GameObject.Find("ElectroCardio").GetComponent<electrocard>().getEndPosition();
        pointUp = new Vector3(-7.5f, 8, 0);
        pointDown = new Vector3(-7.5f, -8, 0);
        lineRenderer.SetPosition(0, pointUp);
        lineRenderer.SetPosition(1, pointDown);
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.startColor = Color.cyan;
        lineRenderer.endColor = Color.cyan;
        lineRenderer.material = myMaterial;
	}
	

}
