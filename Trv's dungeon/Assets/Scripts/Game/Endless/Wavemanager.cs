using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Wavemanager : MonoBehaviour
{
    public float waveDuration;
    public Transform[] itemLocs;
    public GameObject fruit;
    public GameObject sword;
    public GameObject boots;
    [SerializeField] private float difficulty;
    private Endlesspawn spawn;
    public UIman ui;
    void Start()
    {
        spawn = GetComponent<Endlesspawn>();
        StartCoroutine(StartDelay());
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(WaveMan());
    }
    IEnumerator WaveMan()
    {
        for (int i = 1; i < 11; i++)
        {
            ui.WaveTextPlay(i);
            yield return new WaitForSeconds(1f);
            if (i == 1)
            {
                StartCoroutine(WarrSpawn());
            }
            else if (i == 3)
            {
                StartCoroutine(ArchSpawn());
            }
            else if (i == 5)
            {
                Instantiate(sword, itemLocs[0].position, quaternion.identity);
                spawn.SpawnMiniBoss();
                StartCoroutine(FruitSpawn());
            }
            else if (i == 7)
            {
                Instantiate(boots, itemLocs[1].position, quaternion.identity);
                StartCoroutine(MerSpawn());
            }
            else if (i == 9)
            {
                spawn.SpawnMiniBoss();
                StartCoroutine(IncDif());
                StartCoroutine(MiniBossSpawn());
            }
            yield return new WaitForSeconds(waveDuration);
            difficulty = difficulty - 0.5f;

        }
    }
    IEnumerator IncDif()
    {
        while (true)
        {
            if (difficulty > 1f)
            {
                difficulty = difficulty - 0.5f;
            }
            yield return new WaitForSeconds(20f);
        }
    }
    IEnumerator FruitSpawn()
    {
        while (true)
        {
            int x = UnityEngine.Random.Range(2, 5);
            Instantiate(fruit, itemLocs[x].position, quaternion.identity);
            yield return new WaitForSeconds(waveDuration * 2.5f);
        }
    }
    IEnumerator MiniBossSpawn()
    {
        while (true)
        {
            spawn.SpawnMiniBoss();
            yield return new WaitForSeconds(difficulty * 2f);
        }
    }
    IEnumerator MerSpawn()
    {
        while (true)
        {
            spawn.SpawnMermaid();
            yield return new WaitForSeconds(difficulty);
        }
    }
    IEnumerator WarrSpawn()
    {
        while (true)
        {
            spawn.SpawnWarrior();
            yield return new WaitForSeconds(difficulty / 1.5f);
        }
    }
    IEnumerator ArchSpawn()
    {
        while (true)
        {
            spawn.SpawnArcher();
            yield return new WaitForSeconds(difficulty * 1.5f);
        }
    }
    public void End()
    {
        StopAllCoroutines();
    }
}
