using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int enemyHealth;

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
        
        enemyHealth -= damage;
    }

    private void Die()
    { 
        EnemySpawnManager.enemyCount--;

        Destroy(gameObject);
    }
}
