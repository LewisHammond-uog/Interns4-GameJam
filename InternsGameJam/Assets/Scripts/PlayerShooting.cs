using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Shooting
{
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


}
