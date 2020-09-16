using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int enemyHealth;

    public delegate void EnemyHealthEvent();
    public static event EnemyHealthEvent EnemyDamaged;
    public static event EnemyHealthEvent EnemyDeath;

    private void Awake()
    {
        enemyHealth = 100;
    }

    public void TakeDamage(int damage)
    {
        if ((enemyHealth -= damage) <= 0f)
        {
            Die();
        }

        EnemyDamaged?.Invoke();
        enemyHealth -= damage;
    }

    private void Die()
    { 
        EnemySpawnManager.enemyCount--;
        EnemyDeath?.Invoke();

        //When an enemy dies half the timescale for 30 seconds
        StartCoroutine(GameManager.Instance.SlowTimescaleForSetTime(0.25f, 3));

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet;
        if(collision.gameObject.TryGetComponent(out bullet))
        {
            if(bullet.creatorTag != tag)
            {
                TakeDamage((int)bullet.BulletDamage);
            }
        }
    }
}
