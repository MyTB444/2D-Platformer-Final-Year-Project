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
    [SerializeField] protected bool _canJump;
    public int _enemyFragility;
    // Logic
    protected float _currentSpeed;
    protected float distance;
    protected bool _facedRight;
    protected bool _isAlive;
    //components
    protected Transform _target;
    protected Character_audio _audio;
    protected Collider2D _collider;
    protected Rigidbody2D _rigid;
    protected Enemy_animations _enemyAnim;
    protected Player _player;
    protected SpriteRenderer _enemySprite;
    protected virtual void Init()
    {
        _enemyAnim = GetComponentInChildren<Enemy_animations>();
        _audio = GetComponentInChildren<Character_audio>();
        _collider = GetComponent<Collider2D>();
        _audio = GetComponentInChildren<Character_audio>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            distance = Vector2.Distance(transform.position, _target.transform.position);
            OutOfMap();
        }
    }

    protected enum CombatState
    {
        Combat,
        Neutral,
    }
    protected enum MovementState
    {
        Attacking,
        Following,
        Stuned,
    }
    protected CombatState currentCombatState = CombatState.Neutral;
    protected MovementState currentMovementState = MovementState.Stuned;

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
        _isAlive = false;
        currentMovementState = MovementState.Stuned;
        StopAllCoroutines();
        this._collider.enabled = false;
        _enemyAnim.DeadAnimation();

    }

    // Are we in the screen?
    public void OutOfMap()
    {
        if (distance > 50)
        {
            Destroy(this.gameObject);
        }
    }
    protected virtual IEnumerator KnockedBack()
    {
        currentMovementState = MovementState.Stuned;
        yield return new WaitForSeconds(_stunVulnerability);
        _rigid.velocity = new Vector2(0, 0);
        currentMovementState = MovementState.Following;
    }
}
