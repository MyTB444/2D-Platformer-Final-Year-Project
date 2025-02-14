
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Endlesspawn : MonoBehaviour
{
    public Transform[] warrSpawnPoints;
    public Transform[] archSpawnPoints;
    public float difficulty;
    public Enemypool[] archerPool;
    public Enemypool[] warriorsPool;
    public struct EnemyTypeToSpawn
    {
        public Enemypool[] pool;
        public Transform[] locs;
    }
    public EnemyTypeToSpawn warrior;
    public EnemyTypeToSpawn archer;
    private void SetWarriors()
    {
        warrior.pool = warriorsPool;
        warrior.locs = warrSpawnPoints;
        archer.pool = archerPool;
        archer.locs = archSpawnPoints;
    }

    void Start()
    {
        SetWarriors();
        StartCoroutine(WarriorSpawn());
        StartCoroutine(ArcherSpawn());
    }
    IEnumerator WarriorSpawn()
    {
        while (true)
        {
            int x = Random.Range(0, 3);
            SpawnRandomizer1(warrior, x);
            yield return new WaitForSeconds(difficulty);
        }
    }
    IEnumerator ArcherSpawn()
    {
        while (true)
        {
            int x = Random.Range(0, 4);
            SpawnRandomizer1(archer, x);
            yield return new WaitForSeconds(difficulty * 2);
        }
    }
    private void Spawn(Enemypool pool, int x, Transform[] loc)
    {
        GameObject enemy = pool.GetFromPool();
        enemy.transform.position = loc[x].position;
        Enemy enemy1 = enemy.GetComponent<Enemy>();
        enemy1.Setup(pool);
    }
    private void SpawnRandomizer1(EnemyTypeToSpawn type, int r)
    {
        int p = Random.Range(1, 3);
        switch (p)
        {
            case 1:
                Spawn(type.pool[0], r, type.locs);
                break;
            case 2:
                Spawn(type.pool[1], r, type.locs);
                break;
        }
    }
}
