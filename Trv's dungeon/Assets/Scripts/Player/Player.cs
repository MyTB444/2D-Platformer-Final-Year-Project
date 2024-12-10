using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // player stats
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _attackDuration;
    [SerializeField] protected float _stunDuration;
    [SerializeField] protected int _health;
    public float _playerfragility;
    //Logic
    private int rollState = 0;
    private float horizontalInput;
    private bool _canWalk = true;
    protected bool _canClimb;
    private bool _keyFound = false;
    //Component variables for handles
    [SerializeField] private GameObject _key;
    private Character_audio _audio;
    private SpriteRenderer _playerkey;
    private SpriteRenderer _playerSprite;
    [SerializeField] UnityEvent playerIsDead;
    [SerializeField] UnityEvent gameWon;
    private PlayerAnimation _playerAnim;
    private Rigidbody2D _rigid;
    private UIman _uiman;
    void Start()
    {
        //HANDLES
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _playerkey = GameObject.FindWithTag("Playerkey").GetComponent<SpriteRenderer>();
        _uiman = GameObject.FindWithTag("UIman").GetComponent<UIman>();
        _audio = GetComponentInChildren<Character_audio>();
    }
    void Update()
    {

        if (OutOfMap() == true)
        {
            gameWon.Invoke();
        }
        if (_canWalk == true)
        {
            MovementInput();
        }
    }
    enum AirState
    {
        Grounded,
        InAir,
    }
    enum MovementState
    {
        Attacking,
        Rolling,
        Standing,
    }
    AirState currentAirState = AirState.Grounded;
    MovementState currentMovementState = MovementState.Standing;
    // input handler
    void MovementInput()
    {
        GroundCalculate();
        //WALK
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Walk(horizontalInput);
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && currentAirState == AirState.Grounded && currentMovementState == MovementState.Standing)
        {
            Jump();
        }
        //FAST FALL
        if (Input.GetKeyDown(KeyCode.S))
        {
            FastFall();
        }
        //CLIMB
        if (Input.GetKeyDown(KeyCode.W) && _canClimb == true && currentMovementState == MovementState.Standing)
        {
            Climb();
        }
        //ROLL
        if (Input.GetKeyDown(KeyCode.LeftShift) && rollState == 0 && currentMovementState == MovementState.Standing)
        {
            StartCoroutine(Rolling());
        }
        //SWING
        if (Input.GetKeyDown(KeyCode.K) && currentMovementState == MovementState.Standing)
        {
            StartCoroutine(Attack());
        }
        //KEY
        if (Input.GetKeyDown(KeyCode.J) && _keyFound == true)
        {
            SpawnKey();
        }
    }
    //CAN WE JUMP?
    private void GroundCalculate()
    {
        int layerMask = (1 << 6) | (1 << 7);
        RaycastHit2D groundInfo = Physics2D.BoxCast(transform.position, new Vector2(0.65f, 0.2f), 0.0f, Vector2.down, 0.3f, layerMask);
        if (groundInfo.collider != null)
        {
            currentAirState = AirState.Grounded;
        }
        else
        {
            currentAirState = AirState.InAir;
        }

    }
    void Jump()
    {
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
        _playerAnim.JumpAnim();
    }
    void FastFall()
    {
        _playerAnim.ClimbAnimStop();
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce * -1);
    }
    void Climb()
    {
        _rigid.gravityScale = 0;
        _playerAnim.ClimbAnimStart();
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce / 1.5f);
    }
    void Walk(float quick)
    {
        Flip(quick);
        _rigid.velocity = new Vector2(quick * _speed, _rigid.velocity.y);
        _playerAnim.RunAnim(quick);
    }
    //FACING LEFT OR RIGHT?
    void Flip(float move)
    {
        if (move > 0)
        {
            _playerSprite.flipX = false;
        }
        else if (move < 0)
        {
            _playerSprite.flipX = true;
        }
    }
    private bool FacedRight()
    {
        if (_playerSprite.flipX == false)
        {
            return true;
        }
        return false;
    }
    IEnumerator Rolling()
    {
        currentMovementState = MovementState.Rolling;
        rollState = 2;
        _playerAnim.RollAnim();
        _audio.DashAudio();
        _speed = _speed * 1.5f;
        yield return new WaitForSeconds(1.0f);
        _speed = _speed / 1.5f;
        currentMovementState = MovementState.Standing;
        yield return new WaitForSeconds(2.5f);
        rollState = 0;
    }
    IEnumerator Attack()
    {
        currentMovementState = MovementState.Attacking;
        _audio.SwingAudio();
        if (FacedRight() == true)
        {
            _playerAnim.SwingRightAnim();
        }
        else if (FacedRight() == false)
        {
            _playerAnim.SwingLeftAnim();
        }
        yield return new WaitForSeconds(_attackDuration);
        currentMovementState = MovementState.Standing;
    }

    public void TakeDamage()
    {
        if (OutOfMap() == false && currentMovementState != MovementState.Rolling && IsPlayerDead() == false)
        {
            _playerAnim.Damaged();
            _health = _health - 1;
            _uiman.DamageUpdate(_health);
            DamageEffect();
        }
    }
    public bool IsPlayerDead()
    {
        if (_health == 0)
        {
            return true;
        }
        return false;
    }
    private void DamageEffect()
    {
        if (IsPlayerDead() == false)
        {
            _audio.DamageAudio();
            StartCoroutine(Stun(_stunDuration));
        }
        else if (IsPlayerDead() == true)
        {
            StopAllCoroutines();
            _canWalk = false;
            _audio.DeathAudio();
            _playerAnim.DeathAnim();
            playerIsDead.Invoke();
        }
    }
    public IEnumerator Stun(float time)
    {
        _canWalk = false;
        yield return new WaitForSeconds(time);
        _canWalk = true;
    }
    public void EnableCLimb()
    {
        _canClimb = true;
    }
    public void DisableClimb()
    {
        _rigid.gravityScale = 1;
        _playerAnim.ClimbAnimStop();
        _canClimb = false;
    }
    public void EnableKey()
    {
        _keyFound = true;
    }
    protected void SpawnKey()
    {
        _keyFound = false;
        _playerkey.enabled = false;
        if (FacedRight() == true)
        {
            Instantiate(_key, new Vector2(transform.position.x + 0.8f, transform.position.y + 0.3f), Quaternion.identity);
        }
        else if (FacedRight() == false)
        {
            Instantiate(_key, new Vector2(transform.position.x - 0.8f, transform.position.y + 0.3f), Quaternion.identity);
        }
    }
    //Win check
    protected bool OutOfMap()
    {
        if (transform.position.y < -8)
        {
            return true;
        }
        return false;
    }
}
