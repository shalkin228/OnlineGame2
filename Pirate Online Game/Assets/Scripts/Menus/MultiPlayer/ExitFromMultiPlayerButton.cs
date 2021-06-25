using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromMultiPlayerButton : MonoBehaviour
{
    public void OnClickExit()
    {
        PhotonNetwork.Disconnect();
        Application.LoadLevel(0);
    }
}
