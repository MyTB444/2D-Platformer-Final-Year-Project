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
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                base.Update();
                ArcherMovement();
                JumpAway();
            }
        }

    }
    protected override void Init()
    {
        base.Init();
        currentMovementState = MovementState.Following;
    }
    //If jack is close and at the same distance, fire arrow.
    private void ArcherMovement()
    {
        WhereIsPlayer();
        if (currentMovementState == MovementState.Following)
        {
            if (_hasLos == true)
            {
                if (distance < 11)
                {
                    RaycastHit2D upInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.up, _attackDistance * 3, 1 << 3);
                    //RaycastHit2D downInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.down, _attackDistance * 3, 1 << 3);
                    if (upInfo.collider == null)
                    {
                        currentMovementState = MovementState.Attacking;
                        StartCoroutine(Shooting());
                    }
                }
            }
        }
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
        yield return new WaitForSeconds(2.0f);
        _canJump = true;

    }
    // Fire arrow based on sprite flip status
    IEnumerator Shooting()
    {
        _enemyAnim.AttackLeft();
        yield return new WaitForSeconds(0.5f);
        if (_facedRight == true)
        {
            _audio.SwingAudio();
            Instantiate(_arrow, new Vector2(transform.position.x + 0.8f, transform.position.y + 0.3f), Quaternion.identity, gameObject.transform);
        }
        else if (_facedRight == false)
        {
            _audio.SwingAudio();
            Instantiate(_arrow, new Vector2(transform.position.x - 0.8f, transform.position.y + 0.3f), Quaternion.identity, gameObject.transform);
        }
        yield return new WaitForSeconds(_attackDuration);
        currentMovementState = MovementState.Following;
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
}

