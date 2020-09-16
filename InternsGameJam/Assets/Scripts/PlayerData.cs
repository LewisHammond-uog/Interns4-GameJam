using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [Range(0, 100)]
    private float health;
    private const float startHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        //Start with full health
        health = startHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if we have collided with a bullet
        Bullet hitBullet;
        if(collision.gameObject.TryGetComponent<Bullet>(out hitBullet))
        {
            //Don't do damange if we didn't create this
            if(hitBullet.Creator != this.gameObject)
            {
                health -= hitBullet.BulletDamage;
            }
        }
    }
}
