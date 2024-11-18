using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.VisionOS;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _jumpCooldown;
    [SerializeField] protected bool _canJump;
    [SerializeField] protected float _attackDistance;
    protected float _currentSpeed;
    protected bool _isAlive = true;
    protected bool _canAttack = false;
    protected Transform _target;
    protected Collider2D _collider;
    protected Rigidbody2D _rigid;
    protected Enemy_animations _enemyAnim;
    protected SpriteRenderer _enemySprite;

    protected virtual void Init()
    {
        _collider = GetComponent<Collider2D>();
        _enemyAnim = GetComponentInChildren<Enemy_animations>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rigid = GetComponent<Rigidbody2D>();
        _enemySprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        Init();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public virtual void Update()
    {
        //Debug.DrawRay(transform.position, Vector2.left * 0.5f, Color.green);
        //Debug.DrawRay(transform.position, Vector2.right * 0.5f, Color.green);
        if (_isAlive == true)
        {
            Movement();
        }
    }
    public virtual void Movement()
    {
        if (_target.position.x < transform.position.x && _canAttack == false)
        {
            _currentSpeed = _speed * -1;
            _enemySprite.flipX = true;
            StartCoroutine(Walking());
        }

        else if (_target.position.x > transform.position.x && _canAttack == false)
        {
            _currentSpeed = _speed;
            _enemySprite.flipX = false;
            StartCoroutine(Walking());

        }

        //jump
        if (_target.position.y > 0.4f && _canJump == true)
        {
            _canJump = false;
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(WaitForJump());
        }

    }
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
    public void EnemyDead()
    {
        if (_isAlive == true)
        {
            StartCoroutine(Dying());
        }

    }
    IEnumerator Dying()
    {
        this._collider.enabled = false;
        _isAlive = false;
        _enemyAnim.DeadAnimation();
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.0f);
        _canAttack = false;
    }
    public void TakeDamage()
    {
        _health = _health - 1;
        if (_health <= 0)
        {
            EnemyDead();
        }
    }
    //check if it is time to attack
    IEnumerator Walking()
    {
        RaycastHit2D rightInfo = Physics2D.Raycast(transform.position, Vector2.right, _attackDistance, 1 << 3);
        RaycastHit2D leftInfo = Physics2D.Raycast(transform.position, Vector2.left, _attackDistance, 1 << 3);
        _rigid.velocity = new Vector2(_currentSpeed, _rigid.velocity.y);
        _enemyAnim.StartWalking();
        yield return new WaitUntil(() => leftInfo.collider == true || rightInfo.collider == true);
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        _canAttack = true;
        _enemyAnim.StopWalking();
        StartCoroutine(Attack());
    }
}
