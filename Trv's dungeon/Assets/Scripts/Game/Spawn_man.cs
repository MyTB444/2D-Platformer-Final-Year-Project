using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_man : MonoBehaviour
{
    [SerializeField] private Transform _spawnA;
    [SerializeField] private Transform _spawnC;
    [SerializeField] private Transform _spawnB;
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


    IEnumerator GoblinSpawn()
    {
        while (true)
        {
            Instantiate(_goblin, _spawnA.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(_difficulty);
            Instantiate(_goblin, _spawnB.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(_difficulty);
            Instantiate(_goblin, _spawnC.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(_difficulty);
        }

    }
    public void DifficultyIncreased()
    {
        _difficulty = _difficulty - 1;
    }
}
