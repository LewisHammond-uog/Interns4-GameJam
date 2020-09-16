﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPunCallbacks
{

    private GameObject creator;
    private PhotonView PV;
    
    //Property so creator can only be set if we do not have one already
    public GameObject Creator
    {
        get { return creator; }
        set { 
            if (creator == null) {
                creator = value;    
            } 
        }
    }

    public bool EffectedByGameSpeed { get => effectedByGameSpeed; set => effectedByGameSpeed = value; }
    private bool effectedByGameSpeed = true;


    [SerializeField]
    private float baseMoveSpeed; //Movespeed when time scale is 1

    [SerializeField]
    private Rigidbody2D bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        //Warning no creator
        if (creator == null) { Debug.LogWarning("CREATOR HAS NOT BEEN SET FOR BULLET!"); }


        //Set our velocity on start
        if(bulletRB != null)
        {
            UpdateBulletBasedOnGameSpeed();
        }



    }

    /// <summary>
    /// funct triggered by event when the game speed is changed
    /// </summary>
    private void UpdateBulletBasedOnGameSpeed()
    {
        //if (!PV.IsMine)
        //    return;
        //Get the new game speed, mutiply it by the base speed,
        //set veloocity
        float newSpeed = effectedByGameSpeed ? baseMoveSpeed * GameManager.Instance.GameSpeed : baseMoveSpeed;
        UpdateSpeed(newSpeed);
    }

    /// <summary>
    /// Sets a new veolcity while keeping the current direction
    /// </summary>
    private void UpdateSpeed(float speed)
    {
        bulletRB.velocity = bulletRB.transform.right * speed;
    }
    
    private void OnEnable()
    {
        //Sub to event when the game speed is changed
        Debug.Log(GameManager.Instance.GameSpeed);
        GameManager.GameSpeedChanged += UpdateBulletBasedOnGameSpeed;
    }
    private void OnDisable()
    {
        //Unsubbed when destroyed
        GameManager.GameSpeedChanged -= UpdateBulletBasedOnGameSpeed;
    }


    
}
