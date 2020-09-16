using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameObject creator;
    
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

    [SerializeField]
    private float moveSpeed; //Movespeed when time scale is 1

    [SerializeField]
    private Rigidbody2D bulletRB;

    // Start is called before the first frame update
    void Start()
    {
        //Set our velocity on start
        if(bulletRB != null)
        {
            UpdateVelocityInCurrentDir(moveSpeed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets a new veolcity while keeping the current direction
    /// </summary>
    private void UpdateVelocityInCurrentDir(float speed)
    {
        bulletRB.velocity = bulletRB.transform.right * speed;
    }

    /*
    private void OnEnable()
    {
        //Sub to event when the game speed is changed
        GameManager.GameSpeedChanged += GameManager_GameSpeedChanged;
    }
    private void OnDisable()
    {
        //Unsubbed when destroyed
        GameManager.GameSpeedChanged -= GameManager_GameSpeedChanged;
    }

    private void GameManager_GameSpeedChanged()
    {
        throw new System.NotImplementedException();
    }
    */
}
