using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private GUIType type;
    [SerializeField] private GameObject startButton;

    private void Start()
    {
        if (type == GUIType.Waiting)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                startButton.SetActive(true);
                if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
                {
                    startButton.GetComponent<Button>().interactable = true;
                }
            }
        }
        else if (type == GUIType.Playing)
        {

        }
    }

    public void OnClickStartButton()
    {
        PhotonNetwork.LoadLevel("MultiPlayerMap" + (int)PhotonNetwork.CurrentRoom.CustomProperties["Map"]);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (startButton.GetComponent<Button>().interactable == false)
            {
                startButton.GetComponent<Button>().interactable = true;
            }
        }
    }
}

public enum GUIType
{
    Waiting, Playing
}
