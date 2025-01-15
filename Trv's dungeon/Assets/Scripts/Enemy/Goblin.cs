using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Goblin : Enemy
{
    [SerializeField] private GameObject rock;
    [SerializeField] private float spawnDelay;
    private int _canThrow = 1;
    protected override void Init()
    {
        base.Init();
        StartCoroutine(SpawnDelay());
    }
    protected override void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                if (currentCombatState != CombatState.Dead)
                {
                    IsJackHere();
                    base.Update();

                    if (currentCombatState == CombatState.Combat && _enemyAnim != null)
                    {
                        Movement();
                        if (_isABoss == true)
                        {
                            RockThrow();
                        }
                    }
                }
            }
        }
    }
    // Spawn delay allow start method to run before we start executing our code
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        currentMovementState = MovementState.Following;
    }
    protected virtual void Movement()
    {
        if (currentMovementState != MovementState.Stuned)
        {
            //Walk based on jack location.
            if (_hasLos == true)
            {
                if (_target.position.x < transform.position.x && currentMovementState == MovementState.Following)
                {
                    _currentSpeed = _speed * -1;
                    Walking(_currentSpeed);
                }
                else if (_target.position.x > transform.position.x && currentMovementState == MovementState.Following)
                {
                    _currentSpeed = _speed;
                    Walking(_currentSpeed);
                }
            }
            //jump
            if (_target.position.y >= transform.position.y + 0.7f && currentMovementState == MovementState.Following && _canJump == true)
            {
                Jump();
            }
        }
    }
    // Is jack at a reachable place.
    protected void IsJackHere()
    {
        RaycastHit2D upInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.up, _attackDistance * 5, 1 << 3);
        RaycastHit2D downInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.down, _attackDistance * 3, 1 << 3);
        if (_target.position.y > transform.position.y + 4 || distance > 17 && _hasLos == false || upInfo.collider == true || downInfo.collider == true)
        {
            currentCombatState = CombatState.Neutral;
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
            _enemyAnim.StopWalking();
        }
        else if (_target.position.y < transform.position.y + 4 && distance < 17 && upInfo.collider == false && downInfo.collider == false)
        {
            currentCombatState = CombatState.Combat;
        }
    }
    protected void Jump()
    {
        _canJump = false;
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        StartCoroutine(WaitForJump());
    }
    // Move until jack is at an attackable distance. Then start attack animation.
    protected void Walking(float speed)
    {
        RaycastHit2D rightInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.right, _attackDistance, 1 << 3);
        RaycastHit2D leftInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.left, _attackDistance, 1 << 3);
        Flip(speed);
        _rigid.velocity = new Vector2(speed, _rigid.velocity.y);
        _enemyAnim.StartWalking();
        if (leftInfo.collider == true || rightInfo.collider == true)
        {
            if (canAttack == true)
            {
                currentMovementState = MovementState.Attacking;
                canAttack = false;
                StartCoroutine(Attack());
            }
        }
    }
    //Attack based on sprite flip.
    IEnumerator Attack()
    {
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        _enemyAnim.StopWalking();
        yield return new WaitForSeconds(0.1f);
        if (_facedRight == true)
        {
            _audio.SwingAudio();
            _enemyAnim.AttackRight();
        }
        else if (_facedRight == false)
        {
            _audio.SwingAudio();
            _enemyAnim.AttackLeft();
        }
        yield return new WaitForSeconds(_attackDuration);
        canAttack = true;
        if (currentMovementState != MovementState.Stuned)
        {
            currentMovementState = MovementState.Following;
        }
    }
    // Jump cooldown.
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
    private void RockThrow()
    {
        if (currentMovementState == MovementState.Following)
        {
            if (_hasLos == true)
            {
                if (distance < 17)
                {
                    RaycastHit2D upInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.up, _attackDistance * 3, 1 << 3);
                    if (upInfo.collider == null)
                    {
                        if (_canThrow == 1)
                        {
                            currentMovementState = MovementState.Attacking;
                            StartCoroutine(Shooting());
                        }
                    }
                }
            }
        }
    }
    IEnumerator Shooting()
    {
        _canThrow = 0;
        StartCoroutine(KnockedBack(2.1f));
        _enemyAnim.StopWalking();
        _rigid.velocity = new Vector2(0, 0);
        _enemyAnim.RockThrowAnim();
        yield return new WaitForSeconds(1.4f);
        _audio.SwingAudio();
        Instantiate(rock, new Vector2(transform.position.x, transform.position.y + 2.0f), Quaternion.identity, gameObject.transform);
        yield return new WaitForSeconds(8);
        _canThrow = 1;
    }
}
