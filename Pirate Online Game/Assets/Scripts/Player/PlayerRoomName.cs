using Photon.Pun;
using System;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerRoomName : MonoBehaviourPunCallbacks
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
            Debug.Log(PhotonNetwork.NickName);
            nickNameText.text = "Player " + PhotonNetwork.NickName;
        }
        else
        {
            nickNameText.text = "Player " + GetComponent<PhotonView>().Owner.NickName;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        int tempNickOther = Convert.ToInt32(otherPlayer.NickName);
        int tempNick = Convert.ToInt32(PhotonNetwork.NickName);

        if(tempNickOther < tempNick && GetComponent<PhotonView>().IsMine)
        {
            tempNick--;

            PhotonNetwork.NickName = tempNick.ToString();
            Debug.Log(PhotonNetwork.NickName);

            UpdateNickname();
        }
    }
}
