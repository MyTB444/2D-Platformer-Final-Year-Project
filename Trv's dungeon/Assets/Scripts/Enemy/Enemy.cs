using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public abstract class Enemy : MonoBehaviour
{
    // enemy stats
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _jumpCooldown;
    [SerializeField] protected float _attackDistance;
    [SerializeField] protected float _height;
    [SerializeField] protected float _attackDuration;
    [SerializeField] protected float _stunVulnerability;
    [SerializeField] protected bool _canJump;
    private Enemypool pool;
    public int _enemyFragility;
    // Logic
    protected bool _hasLos = false;
    protected bool canAttack = true;
    protected float distance;
    public bool _facedRight;
    protected float _currentSpeed;
    [SerializeField] protected bool _isABoss;
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
        _health = maxHealth;
    }
    protected virtual void Start()
    {
        Init();
    }
    protected virtual void Update()
    {
        distance = Vector2.Distance(transform.position, _target.transform.position);
        FowCheck();
        OutOfMap();
    }
    protected enum CombatState
    {
        Neutral,
        Combat,
        Dead,
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
    protected void FowCheck()
    {
        int layerMask = (1 << 3 | 1 << 7);
        RaycastHit2D fow = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.6f), _target.position - transform.position, distance + 2, layerMask);
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.7f), _target.position - transform.position, Color.green);
        // Debug.Log(fow.collider.tag);
        if (fow.collider != null)
        {
            _hasLos = fow.collider.CompareTag("Player");
        }
    }
    protected void Flip(float move)
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
    public virtual void TakeDamage()
    {
        if (currentCombatState != CombatState.Dead)
        {
            _health = _health - 1;
            currentMovementState = MovementState.Stuned;
            StartCoroutine(KnockedBack(_stunVulnerability));
            _audio.DamageAudio();
            _enemyAnim.Damaged();
            if (_health <= 0)
            {
                EnemyDead();
            }
        }
    }
    public void EnemyDead()
    {
        StopAllCoroutines();
        currentMovementState = MovementState.Stuned;
        currentCombatState = CombatState.Dead;
        _rigid.gravityScale = 1;
        if (_isABoss == false)
        {
            this._collider.enabled = false;
        }
        else if (_isABoss == true)
        {
            _rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        _enemyAnim.DeadAnimation();
    }
    public void Setup(Enemypool objectPool)
    {
        pool = objectPool;
    }
    // Are we in the screen?
    protected void OutOfMap()
    {
        if (distance > 100)
        {
            if ("Game" == SceneManager.GetActiveScene().name)
            {
                Destroy(this.gameObject);
            }
            else if ("Endless" == SceneManager.GetActiveScene().name)
            {
                _health = maxHealth;
                _collider.enabled = true;
                currentCombatState = CombatState.Neutral;
                pool.ReturnToPool(gameObject);
            }
        }
    }
    protected void JumpCheck()
    {
        if (_target.position.y >= transform.position.y + _height && currentMovementState == MovementState.Following && _canJump == true)
        {

            Jump();

        }
    }
    protected void Jump()
    {
        _canJump = false;
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        StartCoroutine(WaitForJump());
    }
    protected IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
    protected virtual IEnumerator KnockedBack(float x)
    {
        currentMovementState = MovementState.Stuned;
        yield return new WaitForSeconds(x);
        _rigid.velocity = new Vector2(0, 0);
        currentMovementState = MovementState.Following;
    }
}
