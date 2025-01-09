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
    public bool _canWalk = true;
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
        GroundCalculate();
       // if (OutOfMap() == true)
       // {
            //Observer trigger for gamewon
           // gameWon.Invoke();
       // }
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
    //CAN WE JUMP? We cast a box from our foot. If it hits an object, we know that we are grounded.
    private void GroundCalculate()
    {
        int layerMask = (1 << 6) | (1 << 7);
        RaycastHit2D groundInfo = Physics2D.BoxCast(transform.position, new Vector2(0.6f, 0.2f), 0.0f, Vector2.down, 0.3f, layerMask);
        if (groundInfo.collider != null)
        {
            currentAirState = AirState.Grounded;
        }
        else
        {
            currentAirState = AirState.InAir;
        }
    }
    public void Jump()
    {
        if (currentAirState == AirState.Grounded && currentMovementState == MovementState.Standing)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _playerAnim.JumpAnim();
        }
    }
    public void FastFall()
    {
        _playerAnim.ClimbAnimStop();
        _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce * -1);
    }
    public void Climb()
    {
        if (_canClimb == true && currentMovementState == MovementState.Standing)
        {
            _rigid.gravityScale = 0;
            _playerAnim.ClimbAnimStart();
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce / 1.5f);
        }
    }
    public void Walk(float quick)
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
    // Return true if we are faced right
    private bool FacedRight()
    {
        if (_playerSprite.flipX == false)
        {
            return true;
        }
        return false;
    }
    public void StartRoll()
    {
        if (rollState == 0 && currentMovementState == MovementState.Standing)
        {
            StartCoroutine(Rolling());
        }
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
    public void StartAttack()
    {
        if (currentMovementState == MovementState.Standing)
        {
            StartCoroutine(Attack());
        }
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
    // Trigger damage take.
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
    // Return true if health is zero
    public bool IsPlayerDead()
    {
        if (_health == 0)
        {
            return true;
        }
        return false;
    }
    // Get stunned when damaged but alive. If we are dead, invoke player is dead event for observers.
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
    // Cant move fore "time" amound of duration
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
    // Instantiate the key object if keyfound is true
    public void SpawnKey()
    {
        if (_keyFound == true)
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
    }
    //Win check
    private bool OutOfMap()
    {
        if (transform.position.y < -8)
        {
            return true;
        }
        return false;
    }
}
