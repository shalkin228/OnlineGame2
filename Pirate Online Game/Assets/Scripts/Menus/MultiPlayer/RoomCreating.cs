using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomCreating : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField roomName, code;
    [SerializeField] private Text statusText;
    [SerializeField] private UnityEvent OnExitRoomCreatingMenu;
    [SerializeField] private int maxLetterCountInInputFields = 8;

    private bool isPublic = true;

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 4;

        Hashtable hash = new Hashtable();
        string[] forLobby = {"pub", "cod"};
        hash.Add("pub", isPublic);
        hash.Add("cod", code.text);

        roomOptions.CustomRoomProperties = hash;
        roomOptions.CustomRoomPropertiesForLobby = forLobby;

        string tempName = null;
        if(roomName.text == "")
        {
            tempName = "_Emp";
        }
        else
        {
            tempName = roomName.text;
        }
 

        PhotonNetwork.CreateRoom(tempName, roomOptions);
    }

    public void ExitRoomCreatingMenu() 
    {
        OnExitRoomCreatingMenu.Invoke();
    }

    public void OnClickStatusButton()
    {
        if (isPublic)
        {
            statusText.text = "Private";
            isPublic = false;
            code.gameObject.SetActive(true);
        }
        else
        {
            statusText.text = "Public";
            isPublic = true;
            code.gameObject.SetActive(false);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 4;

        Hashtable hash = new Hashtable();
        string[] forLobby = { "pub", "cod" };
        hash.Add("pub", isPublic);
        hash.Add("cod", code.text);

        roomOptions.CustomRoomProperties = hash;
        roomOptions.CustomRoomPropertiesForLobby = forLobby;

        PhotonNetwork.CreateRoom(roomName.text + Random.Range(0, 10), roomOptions);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        var hash = PhotonNetwork.CurrentRoom.CustomProperties;

        // what map will all players join in your room
        hash.Add("Map", 1);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

        PhotonNetwork.LoadLevel("MultiPlayerWaiting");
    }

    public void NameInputFieldCheck(string text)
    {
        if (text.Length > maxLetterCountInInputFields)
        {
            roomName.text = text.Remove(text.Length - 1, 1);
            Debug.Log("You can't add more letters to text than " + maxLetterCountInInputFields);
        }
    }

    public void CodeInputFieldCheck(string text)
    {
        if (text.Length > maxLetterCountInInputFields)
        {
            code.text = text.Remove(text.Length - 1, 1);
            Debug.Log("You can't add more letters to text than " + maxLetterCountInInputFields);
        }
    }
}
