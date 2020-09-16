using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static Room room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    [SerializeField] private int multiplayerScene = 1;
    private const string networkPrefabName = "NetworkPlayer";




    private void Awake()
    {
        if (Room.room == null)
            Room.room = this;
        else if (Room.room != this)
        {
            Destroy(Room.room.gameObject);
            Room.room = this;
        }
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }


    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined room!");
        StartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    private void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        PhotonNetwork.LoadLevel(multiplayerScene);
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if (currentScene == multiplayerScene)
            CreatePlayer();

    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", networkPrefabName), transform.position, Quaternion.identity, 0);
    }
}
