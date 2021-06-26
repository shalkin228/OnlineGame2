using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiPlayerMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent onJoinedLobby;
    [SerializeField] private UnityEvent onClickCreateRoom;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        onJoinedLobby.Invoke();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void ClickCreateRoom()
    {
        onClickCreateRoom.Invoke();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("is'nt master");
            PhotonNetwork.LoadLevel("MultiPlayerWaiting");
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log(message);
    }
}
