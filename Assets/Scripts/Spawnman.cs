using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Spawnman : MonoBehaviour
{
    [SerializeField] private float _standarddif = 6.0f;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemySpawn;
    [SerializeField] private float _currentdif;
    private bool _stopSpawn = false;
    void Start()
    {
        StartCoroutine(SpawnEnemy(_standarddif + _currentdif));

    }
    void Update()
    {
    }


    private IEnumerator SpawnEnemy(float waitTime)
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.0f, 9.0f), 9, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemySpawn.transform;
            waitTime = Random.Range(0.0f, 5.0f);
        }
    }
    public void PlayerDeath(){
        _stopSpawn = true;
    }
}




