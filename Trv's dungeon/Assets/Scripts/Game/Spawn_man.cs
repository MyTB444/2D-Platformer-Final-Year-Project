using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Spawn_man : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] public int _difficulty;
    public GameObject _goblin;
    public GameObject _minigoblin;
    public GameObject _goblinarcher;
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
    public void SpawnBossArcher(float x, float y)
    {
        Instantiate(_goblinarcher, new Vector2(x, y), Quaternion.identity);
    }
    public void SpawnMiniBoss(float x, float y)
    {
        Instantiate(_minigoblin, new Vector2(x, y), Quaternion.identity);
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
// Check player location, set state and spawn accordingly.