using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingArrayController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private Vector3 v3Pos;
    private float angle;
    private float dist = 1;


    private void Update()
    {

        //Get the agnle from the mouse to the player
        v3Pos = Input.mousePosition;
        v3Pos.z = player.transform.position.z - Camera.main.transform.position.z;
        v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
        v3Pos = v3Pos - player.transform.position;
        angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;

        //Rotate sprite that is orbiting around us
        transform.localEulerAngles = new Vector3(0, 0, angle);

        //Project angle out in a circle by the given distance
        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * dist;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * dist;

        //Apply Position
        transform.localPosition = new Vector3(player.transform.position.x + xPos, player.transform.position.y + yPos, 0);

    }
}
