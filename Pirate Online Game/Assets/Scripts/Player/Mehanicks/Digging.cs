using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{
    public bool canDig;
    [SerializeField] private GUIType type; 

    public GameObject digHole;

    public void Dig()
    {
        if (canDig && type == GUIType.Playing)
        {
            GetComponent<PhotonView>().RPC("InstantiateDigHole", RpcTarget.All);
            return;
        }
    }

    [PunRPC]
    public void InstantiateDigHole()
    {
        Instantiate(digHole, transform.position, Quaternion.identity);
    }
}
