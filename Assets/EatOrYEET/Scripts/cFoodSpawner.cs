using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFoodSpawner : MonoBehaviour
{

    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private int defaultPoolSize = 5;
    [SerializeField] private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private float curSecondsToSpawn;
    [SerializeField] private float minSecondsToSpawn;
    [SerializeField] private float maxSecondsToSpawn;
    private float secondsPassed;
    private Transform playerTransform;
    private Random rand = new Random();

    void Start()
    {
        playerTransform = Camera.main.transform;
        InitPool();
    }

    void Update()
    {
        secondsPassed += Time.deltaTime;
        if (secondsPassed >= curSecondsToSpawn)
        {
            SpawnFood();
            ResetSecondsToSpawn();
        }
    }

    void ResetSecondsToSpawn()
    {
        secondsPassed = 0;
        curSecondsToSpawn = Random.Range(minSecondsToSpawn, maxSecondsToSpawn);
    }

    void InitPool()
    {
        for (int idx = 0; idx < defaultPoolSize; idx++)
        {
            int index = (int)Random.Range(-1, prefabs.Count);
            GameObject prefab = prefabs[index];
            FoodItem foodItem = prefab.GetComponent<FoodItem>();
            //foodItem.playerTransform = playerTransform;
            GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);

            pool.Add(go);
            go.SetActive(false);
        }
    }

    void SpawnFood()
    {
        GameObject go = null;
        for (int idx = 0; idx < pool.Count; idx++)
        {
            if(!pool[idx].activeSelf)
            {
                go = pool[idx];
            }
        }

        if (go != null)
        {
            go.transform.position = transform.position;
            go.SetActive(true);
            pool.Remove(go);
            pool.Add(go);
        }

        else
        {
            int index = (int)Random.Range(-1, prefabs.Count);
            GameObject prefab = prefabs[index];
            FoodItem foodItem = prefab.GetComponent<FoodItem>();
            //foodItem.playerTransform = playerTransform;
            GameObject newGo = Instantiate(prefab, transform.position, Quaternion.identity);

            pool.Add(newGo);
        }
        // FUNSIES
        // Rigidbody rb = go.GetComponent<Rigidbody>();
        // Vector3 dir = new Vector3(Random.value, Random.value, Random.value);
        // rb?.AddForce( (Vector3.up + dir) * Random.Range(5, 20), ForceMode.Impulse);
    }
}
