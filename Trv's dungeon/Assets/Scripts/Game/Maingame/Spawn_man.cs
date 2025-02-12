using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Spawn_man : MonoBehaviour
{
    [SerializeField] private Transform[] skeletonLocs;
    [SerializeField] private Transform[] archerLocs;
    public GameObject skeletonArcher;
    public GameObject skeletonWarrior;
    public GameObject _minigoblin;
    public GameObject _goblinarcher;

    // Start is called before the first frame update
    //Spawn a goblin in the next spot every few seconds.
    public void SpawnBossArcher(float x, float y)
    {
        Instantiate(_goblinarcher, new Vector2(x, y), Quaternion.identity);
    }
    public void SpawnMiniBoss(float x, float y)
    {
        Instantiate(_minigoblin, new Vector2(x, y), Quaternion.identity);
    }
    public void SpawnFightArcher()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(skeletonArcher, archerLocs[i].position, Quaternion.identity);
        }
    }
    public void SpawnFightSkeleton()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(skeletonWarrior, skeletonLocs[i].position, Quaternion.identity);
        }
    }
    // Decrease the time at wich goblins spawn.
}
// Check player location, set state and spawn accordingly.