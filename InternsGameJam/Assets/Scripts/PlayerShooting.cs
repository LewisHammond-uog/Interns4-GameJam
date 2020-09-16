using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Transform[] bulletSpawns; //Points to spawn the bullets at

    [SerializeField]
    private float shootingCooldown = 0.5f;
    private float timeSinceLastShot = 0f;

    [SerializeField]
    private GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastShot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Increase time since last shot timer
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if(timeSinceLastShot > shootingCooldown)
            {
                Shoot();

                //Reset Timer
                timeSinceLastShot = 0f;
            }
        }
    }

    /// <summary>
    /// Shoots from all of the spawn points
    /// </summary>
    private void Shoot()
    {
        foreach(Transform spawnPoint in bulletSpawns)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.rotation;
        }
    }
}
