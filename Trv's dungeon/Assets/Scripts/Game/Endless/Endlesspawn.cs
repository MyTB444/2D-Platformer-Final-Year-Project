using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Endlesspawn : MonoBehaviour
{
    [SerializeField] private Transform[] warrSpawnPoints;
    public GameObject goblin;
    private float difficulty = 3.0f;
    public Enemypool goblinwarrPool;
    public Enemypool skelwarrPool;
    void Start()
    {
        StartCoroutine(WarriorSpawn());
    }
    IEnumerator WarriorSpawn()
    {
        while (true)
        {
            int r = Random.Range(0, 2);
            int p = Random.Range(1, 3);
            switch (p)
            {
                case 1:
                    Spawn(goblinwarrPool, r);
                    break;
                case 2:
                    Spawn(skelwarrPool, r);
                    break;
            }
            yield return new WaitForSeconds(difficulty);
        }
    }
    private void Spawn(Enemypool pool, int x)
    {
        GameObject enemy = pool.GetFromPool();
        enemy.transform.position = warrSpawnPoints[x].position;
        Enemy enemy1 = enemy.GetComponent<Enemy>();
        enemy1.Setup(goblinwarrPool);
    }
}

