using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private bool setNick;

    void Start()
    {
        if(setNick)
        {
            PhotonNetwork.NickName = PhotonNetwork.CurrentRoom.PlayerCount.ToString();
            Hashtable hash = new Hashtable();
            hash.Add("nick", PhotonNetwork.CurrentRoom.PlayerCount);
            bool b = PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }

        var tempPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos.position, Quaternion.identity);
        tempPlayer.tag = "Player";
        tempPlayer.layer = 7;

        GetComponentInChildren<CinemachineVirtualCamera>().Follow = tempPlayer.transform;

        tempPlayer.GetComponent<PlayerRoomName>().UpdateNickname();
    }
}
