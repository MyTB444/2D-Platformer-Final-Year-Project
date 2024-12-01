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
    protected Collider2D _collider;
    protected Rigidbody2D _rigid;
    protected Enemy_animations _enemyAnim;
    protected SpriteRenderer _enemySprite;

    protected virtual void Init()
    {
        _isAlive = true;
        //handles
        _collider = GetComponent<Collider2D>();
        _enemyAnim = GetComponentInChildren<Enemy_animations>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rigid = GetComponent<Rigidbody2D>();
        _enemySprite = GetComponentInChildren<SpriteRenderer>();
    }
    protected void Start()
    {
        Init();
    }
    protected virtual void Update()
    {
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + _height), Vector2.left * 0.5f, Color.green);
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + _height), Vector2.right * 0.5f, Color.green);
        if (_target.transform.gameObject != null)
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
            _currentSpeed = _speed * -1;
            StartCoroutine(Walking());
        }
        else if (_target.position.x > transform.position.x && _canMove == true && _knockedBack == false && _attacking == false)
        {
            _currentSpeed = _speed;
            StartCoroutine(Walking());
        }
        //jump
        if (_target.position.y >= transform.position.y + 0.7f && _canJump == true && _canMove == true && _knockedBack == false)
        {
            _canJump = false;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(WaitForJump());
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
        if (_target.position.y >= transform.position.y + 3.0f)
        {
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
            _enemyAnim.StopWalking();
            _canMove = false;
        }
        if (_target.position.y < transform.position.y + 1.8f)
        {
            _canMove = true;
        }
    }
    IEnumerator Walking()
    {
        RaycastHit2D rightInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.right, _attackDistance, 1 << 3);
        RaycastHit2D leftInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _height), Vector2.left, _attackDistance, 1 << 3);
        Flip(_currentSpeed);
        _rigid.velocity = new Vector2(_currentSpeed, _rigid.velocity.y);
        _enemyAnim.StartWalking();
        yield return new WaitUntil(() => leftInfo.collider == true || rightInfo.collider == true);
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        _enemyAnim.StopWalking();
        _attacking = true;
        StartCoroutine(Attack());
    }
    //check if it is time to attack
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        if (_facedRight == true)
        {
            _enemyAnim.AttackRight();
        }
        else if (_facedRight == false)
        {
            _enemyAnim.AttackLeft();
        }
        yield return new WaitForSeconds(_attackDuration);
        _attacking = false;
    }
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
    public void TakeDamage()
    {
        _health = _health - 1;
        StopAllCoroutines();
        StartCoroutine(KnockedBack());
        if (_health <= 0)
        {
            EnemyDead();
        }
    }
    public void EnemyDead()
    {
        if (_isAlive == true)
        {
            StopAllCoroutines();
            StartCoroutine(Dying());
        }
    }
    protected virtual IEnumerator Dying()
    {
        this._collider.enabled = false;
        _isAlive = false;
        _enemyAnim.DeadAnimation();
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
    // Are we in the screen?
    public void OutOfMap()
    {
        if (_target.position.y > transform.position.y + 15)
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
}
