using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : Enemy
{
    private Enemy_hitbox _hitboxCollider;
    protected override void Init()
    {
        base.Init();
        _hitboxCollider = GetComponentInChildren<Enemy_hitbox>();
    }
    public override void Update()
    {
        base.Update();
        if (_isAlive == true)
        {
            Movement();
        }
    }
    protected override IEnumerator Dying()
    {
        _hitboxCollider.Disable();
        return base.Dying();
    }
}
