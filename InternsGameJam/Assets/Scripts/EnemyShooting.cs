using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Shooting
{

    //Random time variace, to simulate them waiting to shoot
    [SerializeField] private float randomWaitTime = 0f;
    [SerializeField] private float maxRandomWait = 20f;
    [SerializeField] private float minRandomWait = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastShot = 0f;
        randomWaitTime = GetRandomWaitTime();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if(timeSinceLastShot > randomWaitTime)
        {
            Shoot();
            randomWaitTime = GetRandomWaitTime();
            timeSinceLastShot = 0f;
        }
    }

    private float GetRandomWaitTime()
    {
        return Random.Range((float)minRandomWait, (float)maxRandomWait);
    }
}
