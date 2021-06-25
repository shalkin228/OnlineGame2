using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ServerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text name, players, status;

    private string roomName;

    private bool isPublic;
    private string roomCode;



    public void AddServerInfo(RoomInfo roomInfo)
    {
        if(roomInfo.PlayerCount == 0)
        {
            Destroy(gameObject);
            return;
        }
        if (roomInfo.Name == "_Emp")
        {
            name.text = "";
            roomName = roomInfo.Name;
        }
        else
        {
            name.text = roomInfo.Name;
        }
        players.text = $"{roomInfo.PlayerCount}/4";
        if ((bool)roomInfo.CustomProperties["pub"])
        {
            isPublic = true;
            status.text = "Public";
        }
        else
        {
            isPublic = false;
            roomCode = (string)roomInfo.CustomProperties["cod"];
            status.text = "Private";
        }
    }

    public void OnClick()
    {
        if (isPublic&& PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        else if (!isPublic && PhotonNetwork.IsConnectedAndReady)
        {
            transform.parent.parent.gameObject.SetActive(false);
            transform.parent.parent.
                gameObject.GetComponent<ServerListings>().privateRoomCode.SetActive(true);
            transform.parent.parent.
                gameObject.GetComponent<ServerListings>().privateRoomCode.GetComponent<PrivateRoomCode>().roomCode = roomCode;
            transform.parent.parent.
                gameObject.GetComponent<ServerListings>().privateRoomCode.GetComponent<PrivateRoomCode>().targetRoom = name.text;
        }
    }
}
