using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI : MonoBehaviourPunCallbacks
{
    public static GUI instance;

    [SerializeField] private GUIType type;
    [SerializeField] private GameObject startButton;
    [SerializeField] private Button swordSlot;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (type == GUIType.Waiting)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                UpdateStartButton();
            }
        }
        else if (type == GUIType.Playing)
        {

        }
    }

    public void OnClickStartButton()
    {
        PhotonNetwork.LoadLevel("MultiPlayerMap" + (int)PhotonNetwork.CurrentRoom.CustomProperties["Map"]);
    }

    public void SetSwordSlotActive(bool active)
    {
        swordSlot.interactable = active;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().name == "MultiPlayerWaiting")
        {
            if (startButton.GetComponent<Button>().interactable == false)
            {
                startButton.GetComponent<Button>().interactable = true;
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        if (PhotonNetwork.IsMasterClient && SceneManager.GetActiveScene().name == "MultiPlayerWaiting")
        {
            if (startButton.GetComponent<Button>().interactable == true && PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                startButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void OnClickSwordSlot()
    {
        foreach(PlayerCombat player in FindObjectsOfType<PlayerCombat>())
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                player.ComboHit();
                return;
            }
        }
    }

    private void UpdateStartButton()
    {
        startButton.SetActive(true);
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            startButton.GetComponent<Button>().interactable = true;
        }
    }
}

public enum GUIType
{
    Waiting, Playing
}
