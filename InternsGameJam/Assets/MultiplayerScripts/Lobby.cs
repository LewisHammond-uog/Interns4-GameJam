using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{

    public static Lobby lobby;
    public GameObject startButton;
    public GameObject cancelButton;
    public GameObject offlineButton;

    private void Awake()
    {
        lobby = this;
    }

    void Start()
    {
        cancelButton.SetActive(false);
        startButton.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player connected");
        startButton.SetActive(true);
        cancelButton.SetActive(false);
        offlineButton.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {

        startButton.SetActive(false);
        cancelButton.SetActive(false);
        offlineButton.SetActive(true);
        Debug.Log("Disconnected. Cause: " + cause);
    }

    public void onStartClick()
    {
        Debug.Log("Start clicked");
        startButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room, code: " + returnCode + ", message: " + message);

        //Create room if error (typically because no rooms available)
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room!");
    }

    private void CreateRoom()
    {
        Debug.Log("Creating room...");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4
        };

        PhotonNetwork.CreateRoom("Room " + randomRoomName, roomOps);
        Debug.Log("Room created. Room number: " + randomRoomName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //Retry
        Debug.Log("Failed to create room, code: " + returnCode + ", message: " + message);
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.GetComponent<Button>().interactable = false;
        cancelButton.GetComponentInChildren<Text>().text = "Cancelling...";
        startButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
        cancelButton.GetComponentInChildren<Text>().text = "Cancel";
        cancelButton.GetComponent<Button>().interactable = true;
    }

}
