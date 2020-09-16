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
    protected void Shoot(bool isEnemy)
    {
        foreach (Transform spawnPoint in bulletSpawns)
        {
            if(isEnemy)
            {
                GameObject eBullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "EnemyBullet"), spawnPoint.position, spawnPoint.rotation, 0);
                eBullet.GetComponent<Bullet>().creatorTag = this.gameObject.tag;
            }
            else
            {
                GameObject bullet = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerBullet"), spawnPoint.position, spawnPoint.rotation, 0);
                bullet.GetComponent<Bullet>().creatorTag = this.gameObject.tag;

            }

        }
    }
}
