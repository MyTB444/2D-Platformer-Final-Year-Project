using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Firemage : Enemy
{
    [SerializeField] private Game_man gameman;
    [SerializeField] private GameObject firewall;
    [SerializeField] private GameObject p1fireball;
    [SerializeField] private GameObject p2fireball;
    [SerializeField] private GameObject minifireball;
    [SerializeField] private float fireballinterval;
    [SerializeField] private Transform[] locations;
    public Spawn_man sm;
    private bool damageable = true;
    private bool jackishere = false;
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
                if (currentCombatState != CombatState.Dead)
                {
                    base.Update();
                    WhereIsPlayer();
                    StartFight();
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
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            StartCoroutine(FireBall());
            yield return new WaitForSeconds(fireballinterval);
        }
    }
    private IEnumerator FireBall()
    {
        _enemyAnim.AttackLeft();
        yield return new WaitForSeconds(0.5f);
        FireRotationUpdate();
        Instantiate(p1fireball, new Vector2(transform.position.x + firerotation, transform.position.y + 0.9f), Quaternion.identity, this.gameObject.transform);
    }
    public override void TakeDamage()
    {
        if (damageable == true)
        {
            damageable = false;
            StartCoroutine(ResetDamage());
            base.TakeDamage();
            if (_health > 0)
            {
                StartCoroutine(LocationUpdate(_health));
            }
            if (_health == 2)
            {
                gameman.FightWalls();
                sm.SpawnFightArcher();
            }
            else if (_health == 0)
            {
                gameman.StopAllWalls();
            }
        }
    }
    private IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.3f);
        damageable = true;
    }
    private IEnumerator LocationUpdate(int x)
    {
        _enemyAnim.WalkTrigger();
        _rigid.gravityScale = 0;
        _collider.enabled = false;
        yield return new WaitForSeconds(1f);
        if (x >= 8)
        {
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
                    firewall.SetActive(false);
                    StopAllCoroutines();
                    currentphase = phase.phase2;
                    break;
            }
        }
        else if (x < 8)
        {
            if (x > 2)
            {
                int z = Random.Range(4, 8);
                transform.position = locations[z].position;
            }
            else if (x < 3)
            {
                if (_target.position.x > 17)
                {
                    transform.position = locations[8].position;
                }
                else if (_target.position.x < 17)
                {
                    transform.position = locations[9].position;
                }
            }
        }
        _collider.enabled = true;
        _rigid.gravityScale = 1;
    }
    private void StartFight()
    {
        if (currentphase == phase.phase2 && jackishere == true)
        {
            currentphase = phase.phase3;
            StartCoroutine(LocationUpdate(6));
            StartCoroutine(FightAttack());
        }
    }
    public void PlayerInPlace()
    {
        jackishere = true;
    }
    private IEnumerator FightAttack()
    {
        while (true)
        {
            int attacktype = Random.Range(1, 3);
            if (attacktype == 1)
            {
                _enemyAnim.AttackRight();
                yield return new WaitForSeconds(0.3f);
                Instantiate(minifireball, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                Instantiate(minifireball, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                Instantiate(minifireball, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            }
            else if (attacktype == 2)
            {
                _enemyAnim.AttackLeft();
                yield return new WaitForSeconds(0.5f);
                Instantiate(p2fireball, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            }
            yield return new WaitForSeconds(4f);
        }
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
