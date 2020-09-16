using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private float speedModifier = 0.6f;

    private Rigidbody2D rb;

    private float followRadius = 1.5f;

    [SerializeField] private bool isNormalEnemy;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        if (!isNormalEnemy)
        {
            speedModifier = 0.4f;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameSpeed <= 0f)
        {
            return;
        }

        float step = speedModifier * Time.deltaTime * GameManager.Instance.GameSpeed;

        if (isNormalEnemy)
        {
            if (Vector2.Distance(rb.position, target.position) >= followRadius)
            {
                rb.position = Vector2.MoveTowards(rb.position, target.position, step);
            }
        }
        else
        {
            rb.position = Vector2.MoveTowards(rb.position, target.position, step);
        }
    }
}
