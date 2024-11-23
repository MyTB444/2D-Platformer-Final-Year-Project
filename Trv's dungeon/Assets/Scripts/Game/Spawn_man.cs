using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.EditorScripts;
using UnityEngine;

public class Spawn_man : MonoBehaviour
{
    [SerializeField] private Transform _spawnA;
    [SerializeField] private Transform _spawnC;
    [SerializeField] private Transform _spawnB;
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
            yield return new WaitForSeconds(10.0f);
        }

    }
}
