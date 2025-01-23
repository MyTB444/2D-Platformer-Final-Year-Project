using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Firemage : Enemy
{
    [SerializeField] private GameObject p1fireball;
    [SerializeField] private float fireballinterval;
    [SerializeField] private Transform[] locations;
    private int firerotation = 2;
    private enum phase
    {
        phase1,
        phase2,
        phase3,
    }
    private phase currentphase = phase.phase1;
    protected override void Init()
    {
        base.Init();
        StartCoroutine(FireballPhase1());
    }

    protected override void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                base.Update();
                WhereIsPlayer();
                if (currentCombatState != CombatState.Dead)
                {
                }
            }
        }

    }
    private void FireRotationUpdate()
    {
        if (_facedRight == true)
        {
            firerotation = 2;
        }
        else if (_facedRight == false)
        {
            firerotation = -2;
        }
    }
    private IEnumerator FireballPhase1()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            _enemyAnim.AttackLeft();
            yield return new WaitForSeconds(0.5f);
            FireRotationUpdate();
            Instantiate(p1fireball, new Vector2(transform.position.x + firerotation, transform.position.y + 0.9f), quaternion.identity, this.gameObject.transform);
            yield return new WaitForSeconds(fireballinterval);
        }
    }
    public override void TakeDamage()
    {
        base.TakeDamage();
        if (_health >= 8)
        {
            StartCoroutine(LocationUpdate(_health));
        }
    }
    private IEnumerator LocationUpdate(int x)
    {
        _enemyAnim.WalkTrigger();
        _collider.enabled = false;
        yield return new WaitForSeconds(1f);
        switch (x)
        {
            case 11:
                transform.position = locations[0].position;
                break;
            case 10:
                transform.position = locations[1].position;
                break;
            case 9:
                transform.position = locations[2].position;
                break;
            case 8:
                transform.position = locations[3].position;
                StopCoroutine(FireballPhase1());
                currentphase = phase.phase2;
                break;
        }
        _collider.enabled = true;
    }
    private void WhereIsPlayer()
    {
        if (_target.position.x > transform.position.x)
        {
            Flip(1);
        }
        else if (_target.position.x < transform.position.x)
        {
            Flip(-1);
        }
    }
}
