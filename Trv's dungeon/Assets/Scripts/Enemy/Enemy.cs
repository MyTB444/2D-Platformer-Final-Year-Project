using System.Collections;
using System.Collections.Generic;
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
    protected bool _isAlive = true;
    protected Transform _target;
    protected Collider2D _collider;
    protected Rigidbody2D _rigid;
    protected Enemy_animations _enemyAnim;
    protected SpriteRenderer _enemySprite;

    protected void Start()
    {
        _collider = GetComponent<Collider2D>();
        _enemyAnim = GetComponentInChildren<Enemy_animations>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rigid = GetComponent<Rigidbody2D>();
        _enemySprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public abstract void Update();
    public void Movement()
    {
        if (_target.position.x < transform.position.x)
        {
            _enemySprite.flipX = true;
            _rigid.velocity = new Vector2(_speed * -1, _rigid.velocity.y);
            _enemyAnim.StartWalking();
        }

        else if (_target.position.x > transform.position.x)
        {
            _enemySprite.flipX = false;
            _rigid.velocity = new Vector2(_speed, _rigid.velocity.y);
            _enemyAnim.StartWalking();
        }

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
    public void TakeDamage()
    {
        _health = _health - 1;
        if (_health <= 0)
        {
            EnemyDead();
        }
    }
}
