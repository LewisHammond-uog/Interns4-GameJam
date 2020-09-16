using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform[] bulletSpawns; //Points to spawn the bullets at

    [SerializeField]
    private float shootingCooldown = 0.5f;
    private float timeSinceLastShot = 0f;

    [SerializeField]
    private GameObject bulletPrefab;

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastShot = 0f;

        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;

        //Increase time since last shot timer
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && IsMouseOverGameWindow)
        {
            
            if(timeSinceLastShot > shootingCooldown)
            {
                //Shoot();
                PV.RPC("RPC_Shoot", RpcTarget.AllBuffered);


                //Reset Timer
                timeSinceLastShot = 0f;
            }
        }
    }

    /// <summary>
    /// Shoots from all of the spawn points
    /// </summary>
    [PunRPC]
    private void RPC_Shoot()
    {
        foreach(Transform spawnPoint in bulletSpawns)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.rotation;
        }
    }

    bool IsMouseOverGameWindow { 
        get { return !(0 > Input.mousePosition.x ||
                0 > Input.mousePosition.y ||
                Screen.width < Input.mousePosition.x ||
                Screen.height < Input.mousePosition.y);
        } 
    }


}
