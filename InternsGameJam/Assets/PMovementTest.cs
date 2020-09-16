using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovementTest : MonoBehaviourPunCallbacks, IPunObservable
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
