using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Shooting : MonoBehaviourPunCallbacks
{
    [SerializeField]
    protected Transform[] bulletSpawns; //Points to spawn the bullets at

    [SerializeField]
    protected float shootingCooldown = 0.5f;
    protected float timeSinceLastShot = 0f;

    [SerializeField]
    protected GameObject bulletPrefab;

    /// <summary>
    /// Shoots from all of the spawn points
    /// </summary>
    protected void Shoot()
    {
        foreach (Transform spawnPoint in bulletSpawns)
        {

       //     GameObject bullet = Instantiate(bulletPrefab);
            GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerBullet"), spawnPoint.position, spawnPoint.rotation, 0);
         //   bullet.transform.position = spawnPoint.position;
         //   bullet.transform.rotation = spawnPoint.rotation;
            bullet.GetComponent<Bullet>().creatorTag = this.gameObject.tag;
        }
    }
}
