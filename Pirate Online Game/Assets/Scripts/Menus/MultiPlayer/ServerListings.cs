using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerListings : MonoBehaviourPunCallbacks
{
    public GameObject privateRoomCode;

    [SerializeField] private ServerManager serverListings;
    [SerializeField] private Transform content;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform server in content)
        {
            Destroy(server.gameObject);
        }

        foreach(RoomInfo roomInfo in roomList)
        {
            ServerManager server = Instantiate(serverListings, content);
            server.AddServerInfo(roomInfo);
        }
    }
}
