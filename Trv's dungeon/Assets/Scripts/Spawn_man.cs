using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_man : MonoBehaviour
{
    public GameObject _goblin;
    // Start is called before the first frame update
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
            Instantiate(_goblin, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(5.0f);
        }

    }
}
