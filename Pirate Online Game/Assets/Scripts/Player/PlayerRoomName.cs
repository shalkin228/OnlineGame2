using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoomName : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI nickNameText;

    public void UpdateNickname()
    {
        GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered);
    } 

    [PunRPC]
    public void SetNickName()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            nickNameText.text = "Player " + PhotonNetwork.NickName;
        }
        else
        {
            nickNameText.text = "Player " + GetComponent<PhotonView>().Owner.NickName;
        }
    }
}
