using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class EnemySpawnManager : MonoBehaviour
{
    public static int enemyCount;

    [SerializeField] private GameObject normalEnemyPrefab;
    [SerializeField] private GameObject variationEnemyPrefab;

    private float timer;
    private float coolDown = 2f;

    private Camera camera;

    private float minLeft = -0.1f;
    private float maxLeft = 0f;
    private float minRight = 1f;
    private float maxRight = 1.1f;

    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        
        timer += Time.deltaTime;

        if (timer >= coolDown)
        {
            timer -= coolDown;

            ChooseEnemyToSpawn();
        }
    }

    void SpawnEnemy(GameObject prefab, Vector3 location)
    {
        Instantiate(prefab, location, prefab.transform.rotation);
        enemyCount++;
    }

    Vector3 GetLocation()
    {
        GetRandomCoords();
        return camera.ViewportToWorldPoint(position: new Vector3(x, y,camera.nearClipPlane));
    }

    void GetRandom(ref float f)
    {
        if (Random.value < .5)
        {
            f = Random.Range(minLeft, maxLeft);
        }
        else
        {
            f = Random.Range(minRight, maxRight);
        }
    }

    void ChooseEnemyToSpawn()
    {
        if (Random.value < .9)
        {
            SpawnEnemy(normalEnemyPrefab, GetLocation());
        }
        else
        {
            SpawnEnemy(variationEnemyPrefab, GetLocation());
        }
    }
    
    void GetRandomCoords()
    {
        if (Random.value < .5)
        {
            x = Random.Range(minLeft, minRight);
            GetRandom(ref y);
        }
        else
        {
            y = Random.Range(minLeft, minRight);
            GetRandom(ref x);
        }
    }

    public void KillRandoEnemy()
    {
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>().TakeDamage(100);
    }
}
