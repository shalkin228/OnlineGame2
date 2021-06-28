using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{
    public bool canDig;

    [SerializeField] private GUIType type;
    [SerializeField] private Transform holeSpawnPoint;

    public GameObject digHole;

    public void Dig()
    {
        if (canDig && type == GUIType.Playing)
        {
            GetComponent<PhotonView>().RPC("InstantiateDigHole", RpcTarget.All, digHole);
            return;
        }
    }

    [PunRPC]
    public void InstantiateDigHole(GameObject hole)
    {
        Instantiate(hole, holeSpawnPoint.position, Quaternion.identity);
    }
}
