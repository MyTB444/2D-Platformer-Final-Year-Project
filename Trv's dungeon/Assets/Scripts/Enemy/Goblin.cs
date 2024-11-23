using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Goblin : Enemy
{
    public override void Update()
    {
        base.Update();
        if (_isAlive == true)
        {
            Movement();

        }
    }
}
