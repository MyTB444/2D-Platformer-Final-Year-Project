using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_man : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] public int _difficulty;
    public GameObject _goblin;
    public bool _canSwpan;
    // Start is called before the first frame update
    void Start()
    {
        if (_canSwpan == true)
        {
            StartCoroutine(GoblinSpawn());
        }
    }
    void Update()
    {
        if (_canSwpan == false)
        {
            StopAllCoroutines();
        }
    }
    //Spawn a goblin in the next spot every few seconds.
    IEnumerator GoblinSpawn()
    {
        while (true)
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                Instantiate(_goblin, _spawnPoints[i].position, Quaternion.identity, transform);
                yield return new WaitForSeconds(_difficulty);
            }
        }
    }
    // Decrease the time at wich goblins spawn.
    public void DifficultyIncreased()
    {
        _difficulty = _difficulty - 1;
    }
    public void StopSpawn()
    {
        _canSwpan = false;
    }
}
