using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Mermaid : Enemy
{
    protected override void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                base.Update();
                if (currentCombatState != CombatState.Dead)
                {
                    JumpCheck();
                    Attack();
                }
            }
        }
    }
    protected override void Init()
    {
        base.Init();
        currentMovementState = MovementState.Following;
    }
    private void Attack()
    {
        RaycastHit2D rightInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.right, _attackDistance, 1 << 3);
        RaycastHit2D leftInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.left, _attackDistance, 1 << 3);
        if (currentMovementState == MovementState.Following)
        {
            if (canAttack == true)
            {
                if (rightInfo.collider != null)
                {
                    Flip(1);
                    currentMovementState = MovementState.Attacking;
                    canAttack = false;
                    _enemyAnim.AttackRight();
                    _rigid.gravityScale = 0;
                    _rigid.velocity = new Vector2(_speed, 0);
                    StartCoroutine(AttackReset());
                }
                else if (leftInfo.collider != null)
                {
                    Flip(-1);
                    currentMovementState = MovementState.Attacking;
                    canAttack = false;
                    _enemyAnim.AttackLeft();
                    _rigid.gravityScale = 0;
                    _rigid.velocity = new Vector2(_speed * -1, 0);
                    StartCoroutine(AttackReset());
                }
            }
        }
    }
    private IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(1.0f);
        _rigid.velocity = Vector2.zero;
        _rigid.gravityScale = 1;
        yield return new WaitForSeconds(_attackDuration);
        currentMovementState = MovementState.Following;
        canAttack = true;
    }
}
