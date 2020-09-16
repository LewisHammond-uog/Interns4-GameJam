using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingArray : MonoBehaviour
{

    [SerializeField]
    private GameObject parent;

    [SerializeField]
    protected float orbitDist = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        float angle = GetEnemyToPlayerAngle();
        //Rotate sprite that is orbiting around us
        transform.localEulerAngles = new Vector3(0, 0, angle);

        //Project angle out in a circle by the given distance
        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * orbitDist;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * orbitDist;

        //Apply Position
        transform.position = new Vector3(parent.transform.position.x + xPos * 3, parent.transform.position.y + yPos * 3, 0);

    }

    protected float GetEnemyToPlayerAngle()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) { return 0f; }

        //Get the agnle from the mouse to the player
        Vector3 mouseWorldPos = player.transform.position;
        Vector3 mouseLocalPos = mouseWorldPos - parent.transform.position;
        float angle = Mathf.Atan2(mouseLocalPos.y, mouseLocalPos.x) * Mathf.Rad2Deg;
        return angle;
    }
}
