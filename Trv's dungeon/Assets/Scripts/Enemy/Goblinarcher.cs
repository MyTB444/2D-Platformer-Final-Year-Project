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
        if (_isAlive == true && GameObject.FindGameObjectWithTag("Player") != null)
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
        _isAlive = true;
        _attacking = false;
    }
    //If jack is close and at the same distance, fire arrow.
    private void ArcherMovement()
    {
        if (_isAlive == true && GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (_player.IsPlayerDead() == false)
            {
                WhereIsPlayer();
                if (_target.position.y <= transform.position.y + 0.2 && _target.position.y >= transform.position.y - 0.2 && _target.position.x < transform.position.x + 9 && _target.position.x! > transform.position.x - 9)
                {
                    Shoot();
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
            _currentSpeed = 4f;
            StartCoroutine(JumpingAway(_currentSpeed));
        }
        else if (rightInfo.collider == true && _canJump == true)
        {
            _currentSpeed = -4f;
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
    private void Shoot()
    {
        if (_attacking == false)
        {
            StartCoroutine(Shooting());
        }
    }
    // Fire arrow based on sprite flip status
    IEnumerator Shooting()
    {
        _attacking = true;
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
        _attacking = false;
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

