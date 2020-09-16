using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NetPlayer : MonoBehaviour
{
    private PhotonView PV;

    private const string playerPrefabName = "Player";

    [HideInInspector] public GameObject thisPlayer;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            thisPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs", playerPrefabName),
                Vector3.zero, Quaternion.identity, 0);

        }
    }

    private void OnApplicationQuit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
    }

}
