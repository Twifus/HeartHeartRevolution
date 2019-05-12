using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BeatMove
{
    public float dateOfBirth;
    public GameObject go;
}

public class electrocard : MonoBehaviour
{

    private float counter = 0.0f;
    private int spawn = 10;
    private int index = 0;
    private bool first = true;
    private static float TRAVEL_TIME = 5.0f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;

    public Color c1 = Color.red;
    public Color c2 = Color.green;
    public Players players;

    public Sprite beatSprite1;
    public Sprite beatSprite2;
    private List<BeatMove> livingBeats;

    public Material myMaterial;
    public Game gc;
    public heart H;
    public AudioSource playback;

    private float coordx;
    private float coordy;

    private Gradient gradient;
    public LineRenderer lineRenderer;

    public Vector2 getEndPosition()
    {
        return endPosition;
    }

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        coordx = 0f;
        coordy = 0f;

        float alpha = 1.0f;
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        

        livingBeats = new List<BeatMove>();

        endPosition = new Vector2(-7.5f, 0.0f);
        startPosition = new Vector2(10f, 0.0f);
        direction = endPosition - startPosition;

    }

    float nextBeatValue()
    {
        if (index < gc.getBeatsValue().Count)
            return gc.getBeatsValue()[index++];
        return 0.0f;
    }

    float nextBeatTime()
    {
        if (index < gc.getBeatsTime().Count)
            return gc.getBeatsTime()[index];
        return 0.0f;
    }

    void Update()
    {
        if (!playback.isPlaying && first)
        {
            playback.PlayDelayed(5.1f - nextBeatTime());
            first = false;
        }

        if (!playback.isPlaying && !first && livingBeats.Count == 0)
        {
            players.endOfGame();
        }

        float tempCoordx = nextBeatTime();
        float tempCoordy = nextBeatValue();

        if (tempCoordy > 4f)
        {
            tempCoordy = 4f;
        }

        if (tempCoordx != coordx && tempCoordy != coordy)
        {
            if (livingBeats.Count == 0 || (livingBeats.Count > 0 && (Time.time - livingBeats[livingBeats.Count - 1].dateOfBirth) > 0.1f))
            {

                coordx = tempCoordx;
                coordy = tempCoordy;

                if (Random.Range(0, 2) == 0)
                {
                    coordy = coordy * -1;
                }

                BeatMove bm = new BeatMove();
                bm.go = new GameObject();
                bm.dateOfBirth = Time.time;

                bm.go.AddComponent<SpriteRenderer>();
                if (coordy > 0)
                {
                    bm.go.GetComponent<SpriteRenderer>().sprite = beatSprite1;
                }
                else if (coordy < 0)
                {
                    bm.go.GetComponent<SpriteRenderer>().sprite = beatSprite2;
                }
                bm.go.transform.localScale = new Vector3(0.3f, 0.3f, 1);
                bm.go.transform.position = new Vector3(startPosition.x, coordy, coordy);
                livingBeats.Add(bm);
            }
        }
        
        BeatMove toDestroy = new BeatMove();

        foreach (BeatMove beat in livingBeats)
        {
            float proportion = 1 - (beat.dateOfBirth + TRAVEL_TIME - Time.time) / TRAVEL_TIME;
            beat.go.transform.position = startPosition + proportion * direction + new Vector2(-0.3f * beat.go.GetComponent<SpriteRenderer>().bounds.size.x, beat.go.transform.position.y);

            if (beat.go.transform.position.x <= -8.5)
            {
                if (beat.go.transform.position.y > 0)
                {
                    players.newCircleWiimote(beat);
                }
                if (beat.go.transform.position.y < 0)
                {
                    players.newCircleNunchunk(beat);
                }

                if (beat.go.transform.position.x <= -10)
                {
                    toDestroy = beat;
                }
            }
        }
        livingBeats.Remove(toDestroy);
        Destroy(toDestroy.go);
 
        lineRenderer.material = myMaterial;
        lineRenderer.widthMultiplier = 0.1f;
        List<Vector3> pointsToLineRenderer = new List<Vector3>();
        lineRenderer.colorGradient = gradient;

        pointsToLineRenderer.Add(new Vector3(-10, 0, 0));
        pointsToLineRenderer.Add(endPosition);
        
        foreach (BeatMove Beat in livingBeats)
        {
            pointsToLineRenderer.Add(new Vector3(Beat.go.transform.position.x - 0.2f, 0, 0));
            pointsToLineRenderer.Add(Beat.go.transform.position);
            pointsToLineRenderer.Add(new Vector3(Beat.go.transform.position.x + 0.2f, 0, 0));
        }
        pointsToLineRenderer.Add(startPosition);

        int size = pointsToLineRenderer.Count;
        Vector3[] toRender = new Vector3[size];
        for (int i = 0 ; i < size ; i++)
        {
            toRender[i] = pointsToLineRenderer[i];
        }
        lineRenderer.positionCount = size;
        lineRenderer.SetPositions(toRender);
    }

    public float getDistanceRed()
    {
        foreach (BeatMove beat in livingBeats)
        {
            if (beat.go.transform.position.y > 0)
            {
                return beat.go.transform.position.x - endPosition.x;
            }
        }
        return 1000f;
    }

    public float getDistanceBlue()
    {
        foreach (BeatMove beat in livingBeats)
        {
            if (beat.go.transform.position.y < 0)
            {
                return beat.go.transform.position.x - endPosition.x;
            }
        }
        return 1000f;
    }


}
