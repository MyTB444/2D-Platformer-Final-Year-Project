using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : Enemy
{
    protected override void Update()
    {
        base.Update();
        if (_isAlive == true && GameObject.FindGameObjectWithTag("Player") != null && _enemyAnim != null)
        {
            Movement();
        }
    }
}
