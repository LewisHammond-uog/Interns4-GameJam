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
        Destroy(gameObject);
    }
}
