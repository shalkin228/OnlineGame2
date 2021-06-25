using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PrivateRoomCode : MonoBehaviourPunCallbacks
{
    [HideInInspector]public string roomCode, targetRoom;

    [SerializeField] private InputField roomCodeInputField;
    [SerializeField] private UnityEvent OnClickExitButton;

    public void OnClickJoinButton()
    {
        if(roomCode.ToLower() == roomCodeInputField.text.ToLower())
        {
            PhotonNetwork.JoinRoom(targetRoom);
            print("code is correct");
        }
    }

    public void ClickExitButton()
    {
        OnClickExitButton.Invoke();
    }
}
