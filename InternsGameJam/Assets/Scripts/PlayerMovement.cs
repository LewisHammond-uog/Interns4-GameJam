using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        if (playerRB == null)
        {
            Debug.LogError("PLAYER DOES NOT HAVE A RIGID BODY");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = GetMovementInput();
        playerRB.AddForce(moveDir * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);

    }

    private Vector3 GetMovementInput()
    {
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += Vector3.up;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += Vector3.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDir += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir += Vector3.right;
        }

        moveDir = moveDir.magnitude != 0 ? moveDir.normalized : moveDir;
        return moveDir;
    }
}
