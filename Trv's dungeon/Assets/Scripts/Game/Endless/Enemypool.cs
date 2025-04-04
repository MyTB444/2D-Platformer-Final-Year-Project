using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemypool : MonoBehaviour
{
    public GameObject goblin;  
    public int poolSize = 10;  
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // Populate the pool with inactive objects
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(goblin);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    // Return the object from pool, if there is none left, instantiate it
    public GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(goblin);
            return obj;
        }
    }
    // Return the object to pool
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}

