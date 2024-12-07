using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : Enemy
{
    protected override void Init()
    {
        base.Init();
        StartCoroutine(SpawnDelay());
    }
    protected override void Update()
    {
        if (_isAlive == true && GameObject.FindGameObjectWithTag("Player") != null && _enemyAnim != null)
        {
            base.Update();
            Movement();
        }
    }
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _isAlive = true;
    }
}
