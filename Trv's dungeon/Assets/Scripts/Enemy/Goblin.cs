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
        IsJackHere();
        if (currentCombatState == CombatState.Combat && GameObject.FindGameObjectWithTag("Player") != null && _enemyAnim != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                base.Update();
                Movement();
            }
        }
    }
    // Spawn delay allow start method to run before we start executing our code
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        currentMovementState = MovementState.Following;
    }
    protected virtual void Movement()
    {
        //Walk based on jack location.

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
        //jump
        if (_target.position.y >= transform.position.y + 0.7f && currentMovementState == MovementState.Following && _canJump == true)
        {
            Jump();
        }
    }
    // Is jack at a reachable place.
    protected void IsJackHere()
    {
        RaycastHit2D upInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.up, _attackDistance * 3, 1 << 3);
        RaycastHit2D downInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.down, _attackDistance * 3, 1 << 3);
        if (_target.position.y >= transform.position.y + 4.5f || upInfo.collider == true || downInfo.collider == true)
        {
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
            _enemyAnim.StopWalking();
            currentCombatState = CombatState.Neutral;
        }
        else if (_target.position.y < transform.position.y + 4.5f && upInfo.collider == false && downInfo.collider == false)
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
            currentMovementState = MovementState.Attacking;
            StartCoroutine(Attack());
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
        currentMovementState = MovementState.Following;
    }
    // Jump cooldown.
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
    protected override IEnumerator KnockedBack()
    {
        currentMovementState = MovementState.Stuned;
        yield return new WaitForSeconds(_stunVulnerability);
        currentMovementState = MovementState.Following;
    }
}


// Check player state is in another map stop movement.
