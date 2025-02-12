using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlesspawn : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    public GameObject goblin;
    public Enemypool enemyPool;
    void Start()
    {
        StartCoroutine(GoblinSpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator GoblinSpawn()
    {
        while (true)
        {
            GameObject enemy = enemyPool.GetFromPool();
            enemy.transform.position = _spawnPoints[0].position;
            Enemy enemy1 = enemy.GetComponent<Enemy>();
            enemy1.Setup(enemyPool);
            yield return new WaitForSeconds(2f);
        }
    }
}

