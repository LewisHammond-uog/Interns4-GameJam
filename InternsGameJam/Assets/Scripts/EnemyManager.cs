using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class EnemyManager : MonoBehaviour
{
    private int enemyCount;

    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject variationEnemyPrefab;

    private float timer;
    private float coolDown = 2f;

    private Camera camera;

    private float minLeft = -0.1f;
    private float maxLeft = 0f;
    private float minRight = 1f;
    private float maxRight = 1.1f;

    private float half = .5f;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= coolDown)
        {
            timer -= coolDown;
            
            SpawnEnemy(normalEnemyPrefab, GetRandomLocation());
        }
    }

    void SpawnEnemy(GameObject prefab, Vector3 location)
    {
        Instantiate(prefab, location, prefab.transform.rotation);
        enemyCount++;
    }

    Vector3 GetRandomLocation()
    {
        return camera.ViewportToWorldPoint(position: new Vector3(GetRandomValue(), GetRandomValue(), camera.nearClipPlane));
    }

    float GetRandomValue()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        float value;
        
        if (Random.value < half)
        {
            value = Random.Range(minLeft, maxLeft);
        }
        else
        {
            value = Random.Range(minRight, maxRight);
        }

        return value;
    }
}
