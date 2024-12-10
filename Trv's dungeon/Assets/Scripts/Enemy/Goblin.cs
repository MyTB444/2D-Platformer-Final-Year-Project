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

        if (_isAlive == true && GameObject.FindGameObjectWithTag("Player") != null && _enemyAnim != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                base.Update();
                Movement();
            }
        }

    }
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _isAlive = true;
    }
    protected virtual void Movement()
    {
        //Walk
        CanWeMove();
        if (_target.position.x < transform.position.x && _canMove == true && _knockedBack == false && _attacking == false)
        {
            //StopCoroutine(Walking(_currentSpeed));
            _currentSpeed = _speed * -1;
            Walking(_currentSpeed);
        }
        else if (_target.position.x > transform.position.x && _canMove == true && _knockedBack == false && _attacking == false)
        {
            // StopCoroutine(Walking(_currentSpeed));
            _currentSpeed = _speed;
            Walking(_currentSpeed);
        }
        //jump
        if (_target.position.y >= transform.position.y + 0.7f && _canJump == true && _canMove == true && _knockedBack == false)
        {
            Jump();
        }
    }
    protected void CanWeMove()
    {
        RaycastHit2D upInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.up, _attackDistance * 3, 1 << 3);
        RaycastHit2D downInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.down, _attackDistance * 3, 1 << 3);
        if (_target.position.y >= transform.position.y + 3.0f || upInfo.collider == true || downInfo.collider == true)
        {
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
            _enemyAnim.StopWalking();
            _canMove = false;
        }
        else if (_target.position.y < transform.position.y + 1.8f && upInfo.collider == false && downInfo.collider == false)
        {
            _canMove = true;
        }
    }
    protected void Jump()
    {
        _canJump = false;
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        StartCoroutine(WaitForJump());
    }
    protected void Walking(float speed)
    {
        RaycastHit2D rightInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.right, _attackDistance, 1 << 3);
        RaycastHit2D leftInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.left, _attackDistance, 1 << 3);
        Flip(speed);
        _rigid.velocity = new Vector2(speed, _rigid.velocity.y);
        _enemyAnim.StartWalking();
        if (leftInfo.collider == true || rightInfo.collider == true)
        {
            StartAttack();
        }
    }
    //check if it is time to attack
    IEnumerator Attack()
    {
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
        _attacking = false;
    }
    protected void StartAttack()
    {
        _attacking = true;
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        _enemyAnim.StopWalking();
        StartCoroutine(Attack());
    }
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
}
