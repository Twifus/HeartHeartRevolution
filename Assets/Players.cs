using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Players : MonoBehaviour {
    public GameObject beatPrefab;

    private int ZERO_NUNCHUNK = 533;
    private int ZERO_WIIMOTE = 533;
    private int WIIMOTE_TH = 250;
    private int NUNCHUNK_TH = 150;
    private float MAX_DIST_SCORE = 1.5f;
    private int SCORE_MULTIPLIER = 100;

    private Wiimote[] remotes = new Wiimote[4];
    private int[] scores = new int[4];
    private bool[] rdyToPlayWiimote = new bool[4];
    private bool[] rdyToPlayNunchunk = new bool[4];
    private int nbRemotes = 0;
    private bool initialized = false;
    private GameObject beat;
    private BeatMove lastBeatNun;
    private BeatMove lastBeatWii;

    private void Update()
    {
        if (!initialized) {
            return;
        }

        for (int i = 0; i < nbRemotes; i++) {
            Wiimote remote = remotes[i];
            int ret;
            do
            {
                ret = remote.ReadWiimoteData();
            } while (ret > 0);

            if (DetectMoveWiimote(remote.Accel.accel) && rdyToPlayWiimote[i])
            {
                float dist = beat.GetComponentInChildren<electrocard>().getDistanceRed();
                calculateScore(dist, i);
                rdyToPlayWiimote[i] = false;
            }

            if (remote.current_ext == ExtensionController.NUNCHUCK)
            {
                if (DetectMoveWiimote(remote.Nunchuck.accel) && rdyToPlayNunchunk[i])
                {
                    float dist = beat.GetComponentInChildren<electrocard>().getDistanceBlue();
                    calculateScore(dist, i);
                    rdyToPlayNunchunk[i] = false;
                }
            }
        }
    }

    private void calculateScore(float dist, int i)
    {
        if (Mathf.Abs(dist) < MAX_DIST_SCORE)
        {
            int score = (int) (((MAX_DIST_SCORE - Mathf.Abs(dist)) / MAX_DIST_SCORE) * SCORE_MULTIPLIER);
            scores[i] += score;
        }
    }

    private bool DetectMoveWiimote(WiimoteApi.Util.ReadOnlyArray<int> accel)
    {
        for (int i = 0; i < accel.Length; i++)
        {
            if (Mathf.Abs(accel[i] - ZERO_WIIMOTE) > WIIMOTE_TH)
            {
                return true;
            }
        }
        return false;
    }

    private bool DetectMoveNunchunk(WiimoteApi.Util.ReadOnlyArray<int> accel)
    {
        for (int i = 0; i < accel.Length; i++)
        {
            if (Mathf.Abs(accel[i] - ZERO_NUNCHUNK) > NUNCHUNK_TH)
            {
                return true;
            }
        }
        return false;
    }

    //----------------------------------------------------

    public int[] getScores()
    {
        return scores;
    }

    public void newCircleWiimote(BeatMove beat)
    {
        if (beat.go == lastBeatWii.go)
            return;
        lastBeatWii = beat;
        for (int i = 0; i < nbRemotes; i++)
        {
            rdyToPlayWiimote[i] = true;
        }
    }

    public void newCircleNunchunk(BeatMove beat)
    {
        if (beat.go == lastBeatNun.go)
            return;
        lastBeatNun = beat;
        for (int i = 0; i < nbRemotes; i++)
        {
            rdyToPlayNunchunk[i] = true;
        }
    }

    public int getNbPlayers()
    {
        return nbRemotes;
    }

    public void endOfGame()
    {
        Destroy(beat);
        initialized = false;
        nbRemotes = 0;
        GetComponent<Menu>().reset();
    }

    public void init()
    {
        if (initialized)
        {
            return;
        }
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            remotes[nbRemotes] = remote;
            scores[nbRemotes] = 0;
            rdyToPlayWiimote[nbRemotes] = true;
            rdyToPlayNunchunk[nbRemotes] = true;
            scores[nbRemotes] = 0;
            nbRemotes++;
        }
        beat = Instantiate(beatPrefab);
        beat.GetComponentInChildren<electrocard>().players = this;
        initialized = true;
    }
}
