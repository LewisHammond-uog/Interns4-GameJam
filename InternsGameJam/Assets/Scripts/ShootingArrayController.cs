﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingArrayController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    protected float orbitDist = 1.5f;

    private PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        float angle = GetMouseToPlayerAngle(); 
        //Rotate sprite that is orbiting around us
        transform.localEulerAngles = new Vector3(0, 0, angle);

        //Project angle out in a circle by the given distance
        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * orbitDist;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * orbitDist;

        //Apply Position
        transform.position = new Vector3(player.transform.position.x + xPos * 3, player.transform.position.y + yPos * 3, 0);

    }

    protected float GetMouseToPlayerAngle()
    {
        //Get the agnle from the mouse to the player
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = player.transform.position.z - Camera.main.transform.position.z;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 mouseLocalPos = mouseWorldPos - player.transform.position;
        float angle = Mathf.Atan2(mouseLocalPos.y, mouseLocalPos.x) * Mathf.Rad2Deg;
        return angle;
    }
}
