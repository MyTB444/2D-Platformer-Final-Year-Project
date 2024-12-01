using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Goblinarcher : Enemy
{
    [SerializeField] private GameObject _arrow;
    protected override void Update()
    {
        base.Update();
        ArcherMovement();
    }
    protected override void Init()
    {
        base.Init();
        _attacking = false;
    }
    private void ArcherMovement()
    {
        if (_isAlive == true && _target.transform.gameObject != null)
        {
            WhereIsPlayer();
            if (_target.position.y <= transform.position.y + 0.2 && _target.position.y >= transform.position.y - 0.2)
            {
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        if (_attacking == false && _knockedBack == false)
        {
            StartCoroutine(Shooting());
        }
    }
    IEnumerator Shooting()
    {
        _attacking = true;
        _enemyAnim.AttackLeft();
        yield return new WaitForSeconds(0.5f);
        if (_facedRight == true)
        {
            Instantiate(_arrow, new Vector2(transform.position.x + 0.8f, transform.position.y + 0.3f), Quaternion.identity, gameObject.transform);
        }
        else if (_facedRight == false)
        {
            Instantiate(_arrow, new Vector2(transform.position.x - 0.8f, transform.position.y + 0.3f), Quaternion.identity, gameObject.transform);
        }
        yield return new WaitForSeconds(_attackDuration);
        _attacking = false;
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

