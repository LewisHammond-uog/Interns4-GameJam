using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Shooting
{
    // Start is called before the first frame update
    private PhotonView PV;


    void Start()
    {
        PV = GetComponent<PhotonView>();
        timeSinceLastShot = 0f;
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
            if (timeSinceLastShot > shootingCooldown)
            {
                Shoot();

                //Reset Timer
                timeSinceLastShot = 0f;
            }
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
