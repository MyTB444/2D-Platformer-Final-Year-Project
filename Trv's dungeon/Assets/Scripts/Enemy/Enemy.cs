using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public abstract class Enemy : MonoBehaviour
{
    // enemy stats
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _jumpCooldown;
    [SerializeField] protected float _attackDistance;
    [SerializeField] protected float _height;
    [SerializeField] protected float _attackDuration;
    [SerializeField] protected float _stunVulnerability;
    [SerializeField] protected bool _isanarcher;
    [SerializeField] protected bool _canJump;
    public int _enemyFragility;
    protected float _currentSpeed;
    protected bool _knockedBack = false;
    protected bool _facedRight;
    protected bool _canMove = true;
    protected bool _isAlive;
    protected bool _attacking = false;
    //components for handles
    protected Transform _target;
    protected Character_audio _audio;
    protected Collider2D _collider;
    protected Rigidbody2D _rigid;
    protected Enemy_animations _enemyAnim;
    protected SpriteRenderer _enemySprite;

    protected virtual void Init()
    {
        _enemyAnim = GetComponentInChildren<Enemy_animations>();
        _audio = GetComponentInChildren<Character_audio>();
        _collider = GetComponent<Collider2D>();
        _audio = GetComponentInChildren<Character_audio>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        _rigid = GetComponent<Rigidbody2D>();
        _enemySprite = GetComponentInChildren<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        Init();
    }
    protected virtual void Update()
    {
        // Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + _height), Vector2.left, Color.green);
        // Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + _height), Vector2.right, Color.green);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            OutOfMap();
        }
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
    //where are we faced
    public void Flip(float move)
    {
        if (move > 0)
        {
            _enemySprite.flipX = false;
            _facedRight = true;
        }
        else if (move < 0)
        {
            _enemySprite.flipX = true;
            _facedRight = false;
        }
    }
    public void CanWeMove()
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
    public void TakeDamage()
    {
        _health = _health - 1;
        StartCoroutine(KnockedBack());
        _audio.DamageAudio();
        _enemyAnim.Damaged();
        if (_health <= 0)
        {
            EnemyDead();
        }
    }
    public void EnemyDead()
    {
        if (_isAlive == true)
        {
            _isAlive = false;
            StopAllCoroutines();
            StartCoroutine(Dying());
        }
    }
    protected virtual IEnumerator Dying()
    {
        this._collider.enabled = false;
        _enemyAnim.DeadAnimation();
        yield return new WaitForSeconds(1.2f);
    }
    // Are we in the screen?
    public void OutOfMap()
    {
        if (_target.position.y > transform.position.y + 15 || _target.position.y < transform.position.y - 15)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator KnockedBack()
    {
        _knockedBack = true;
        yield return new WaitForSeconds(_stunVulnerability);
        if (_isanarcher == true)
        {
            _rigid.velocity = new Vector2(0, 0);
        }
        _knockedBack = false;
        _canMove = true;
        _attacking = false;
    }
    protected void Jump()
    {
        _canJump = false;
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        StartCoroutine(WaitForJump());
    }
}
