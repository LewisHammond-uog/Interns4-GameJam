using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private float speedModifier = 0.4f;

    private Rigidbody2D rb;

    private float followRadiusMin = 1.75f;
    private float followRadiusMax = 2f;

    private float thisFollowRadius;

    [SerializeField] private bool isNormalEnemy;

    private bool isWaiting;

    private float continueFollowTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        thisFollowRadius = Random.Range(followRadiusMin, followRadiusMax);

        if (!isNormalEnemy)
        {
            speedModifier = 0.5f;
        }
    }

    private void FixedUpdate()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;

        if (GameManager.Instance.GameSpeed <= 0f || target == null)
        {
            return;
        }

        float step = speedModifier * Time.deltaTime * GameManager.Instance.GameSpeed;

        if (isNormalEnemy)
        {
            if (Vector2.Distance(rb.position, target.position) >= thisFollowRadius && !isWaiting)
            {
                rb.position = Vector2.MoveTowards(rb.position, target.position, step);
            }
            else if (Vector2.Distance(rb.position, target.position) >= thisFollowRadius * continueFollowTime && isWaiting)
            {
                FlipBool(isWaiting);
            }
        }
        else
        {
            rb.position = Vector2.MoveTowards(rb.position, target.position, step);
        }
    }

    void FlipBool(bool b)
    {
        b = !b;
    }

}
