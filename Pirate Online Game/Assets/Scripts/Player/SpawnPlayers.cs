using Cinemachine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPos;

    void Start()
    {
        var tempPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos.position, Quaternion.identity);
        tempPlayer.tag = "Player";
        tempPlayer.layer = 7;

        GetComponentInChildren<CinemachineVirtualCamera>().Follow = tempPlayer.transform;
    }
}
