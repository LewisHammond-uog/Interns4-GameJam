using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovementTest : MonoBehaviourPun
{
    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            PV.RPC("RPC_Moving", RpcTarget.AllBuffered);
        }
    }



    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
        Debug.Log(info);
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        if (Input.GetKey(KeyCode.Space))
        {
            PV.RPC("RPC_Moving", RpcTarget.AllBuffered);
        }
        
    }


    [PunRPC]
    void RPC_Moving()
    {
        transform.Rotate(Vector3.right, Space.Self);
    }
}
