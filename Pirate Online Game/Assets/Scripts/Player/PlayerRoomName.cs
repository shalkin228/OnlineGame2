using Photon.Pun;
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

        if ((byte)otherPlayer.CustomProperties["nick"] < (byte)PhotonNetwork.LocalPlayer.CustomProperties["nick"])
        {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
            byte tempNick = (byte)hash["nick"];
            hash.Remove("nick");
            hash.Add("nick", tempNick--);

            PhotonNetwork.NickName = tempNick--.ToString();
            UpdateNickname();
        }
    }
}
