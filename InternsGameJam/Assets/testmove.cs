using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class testmove : MonoBehaviour
{
    private Rigidbody2D rb;
    private float x;
    private float y;

    private float speedModifier = 30f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 inputVelocity = new Vector2(x, y);
        
        rb.velocity = inputVelocity * (speedModifier * Time.deltaTime);
    }
}
