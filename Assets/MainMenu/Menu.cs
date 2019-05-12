using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiimoteApi;

public class Menu : MonoBehaviour {

    public Players players;

    private int nPlayers;
    private bool readyToPlay = false;
    private bool playing = false;
	
	private void Update () {
        if (playing)
        {
            return;
        }
        ConnectWiimotes();

        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            if (remote.Button.a && readyToPlay)
            {
                players.init();
                playing = true;
                GetComponent<Canvas>().enabled = false;
                return;
            }
            else if (remote.Button.b)
            {
                Application.Quit();
            }
        }
	}

    private void ConnectWiimotes()
    {
        WiimoteManager.FindWiimotes();

        nPlayers = 0;

        if (!WiimoteManager.HasWiimote())
        {
            readyToPlay = false;
            return;
        }

        readyToPlay = true;

        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            remote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL_EXT16);

            int ret;
            do
            {
                ret = remote.ReadWiimoteData();
            } while (ret > 0);

            remote.SendPlayerLED(nPlayers == 0, nPlayers == 1, nPlayers == 2, nPlayers == 3);
            nPlayers++;
            if (remote.current_ext != ExtensionController.NUNCHUCK)
            {
                readyToPlay = false;
            }
        }
        
    }

    public int GetNumberPlayers()
    {
        return nPlayers;
    }

    public bool IsReadyToPlay()
    {
        return readyToPlay;
    }

    public void reset()
    {
        playing = false;
        readyToPlay = false;
        GetComponent<Canvas>().enabled = true;
    }
}
