﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private PhotonView PV;

    [Range(0, 100)]
    private float health;
    private float Health {
        get { return health; }
        set { 
            //Trigger comparision events
            if(health < value) { PlayerHealed?.Invoke(); }
            if(health > value) { PlayerDamaged?.Invoke(); }

            //Update health
            health = value;

            //Trigger death event
            if (health <= 0) { PlayerDeath?.Invoke();  }

        }
    }

    private const float startHealth = 100;

    //Event for player being damaged
    public delegate void PlayerHealthEvent();
    public event PlayerHealthEvent PlayerDamaged;
    public event PlayerHealthEvent PlayerHealed;
    public event PlayerHealthEvent PlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        //Start with full health
        Health = startHealth;
        PV = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if we have collided with a bullet
        Bullet hitBullet;
        if (collision.gameObject.TryGetComponent<Bullet>(out hitBullet))
        {
            //Don't do damange if we didn't create this
            if (hitBullet.creatorTag != this.gameObject.tag)
            {
                Health -= hitBullet.BulletDamage;
                PlayerDamaged?.Invoke();
                Debug.Log(health);
            }
        }
    }

    private void DestoryPlayer()
    {
        Destroy(gameObject);
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        PlayerDeath += DestoryPlayer;
    }
    private void OnDisable()
    {
        PlayerDeath -= DestoryPlayer;
    }
}
