using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Goblinarcher : Enemy
{
    [SerializeField] private float attackdelay;
    [SerializeField] private GameObject _arrow;
    protected override void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                base.Update();
                if (currentCombatState != CombatState.Dead)
                {
                    ArcherMovement();
                    JumpAway();
                }
            }
        }
    }

    protected override void Init()
    {
        base.Init();
        currentMovementState = MovementState.Following;
    }
    // Check jacks location and attack if he is close
    private void ArcherMovement()
    {
        WhereIsPlayer();
        if (currentMovementState == MovementState.Following)
        {
            if (_hasLos == true)
            {
                if (distance < 17)
                {
                    RaycastHit2D upInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.up, _attackDistance * 3, 1 << 3);
                    //RaycastHit2D downInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.down, _attackDistance * 3, 1 << 3);
                    if (upInfo.collider == null)
                    {
                        if (canAttack == true)
                        {
                            canAttack = false;
                            currentMovementState = MovementState.Attacking;
                            StartCoroutine(Shooting());
                        }
                    }
                }
            }
        }
    }
    // Flip the sprite based on jack location.
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
    // Fire arrow based on sprite flip status
    IEnumerator Shooting()
    {
        _enemyAnim.AttackLeft();
        yield return new WaitForSeconds(attackdelay);
        Instantiate(_arrow, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity, gameObject.transform);
        _audio.SwingAudio();
        yield return new WaitForSeconds(_attackDuration);
        currentMovementState = MovementState.Following;
        canAttack = true;
    }
    // Use rays to detect surroundings, if jack is close jump away.
    private void JumpAway()
    {
        RaycastHit2D rightInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.right, _attackDistance, 1 << 3);
        RaycastHit2D leftInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.left, _attackDistance, 1 << 3);
        if (leftInfo.collider == true && _canJump == true)
        {
            _currentSpeed = _speed;
            StartCoroutine(JumpingAway(_currentSpeed));
        }
        else if (rightInfo.collider == true && _canJump == true)
        {
            _currentSpeed = _speed * -1;
            StartCoroutine(JumpingAway(_currentSpeed));
        }
    }
    private IEnumerator JumpingAway(float speed)
    {
        _canJump = false;
        _rigid.velocity = new Vector2(speed, 4f);
        _enemyAnim.WalkTrigger();
        _audio.JumpAudio();
        yield return new WaitForSeconds(0.4f);
        _rigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;

    }
}

