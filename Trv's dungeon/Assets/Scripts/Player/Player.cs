using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
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
    private float horizontalInput;
    //Logic
    private int _canRoll = 0;
    private bool _canWalk = true;
    protected bool _canClimb;
    private bool _facedRight;
    private bool _duringRoll;
    private bool _playerDamageable;
    private bool _resetJump = false;
    private bool _attacking = false;
    private bool _keyFound = false;
    //Component variables for handles
    [SerializeField] private GameObject _key;
    private Character_audio _audio;
    private SpriteRenderer _playerkey;
    private SpriteRenderer _playerSprite;
    private PlayerAnimation _playerAnim;
    private Rigidbody2D _rigid;
    private UIman _uiman;
    private Game_man _gameman;
    private Spawn_man _spawnman;
    void Start()
    {
        _playerDamageable = true;
        //HANDLES
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _spawnman = GameObject.FindWithTag("Spawnman").GetComponent<Spawn_man>();
        _playerkey = GameObject.FindWithTag("Playerkey").GetComponent<SpriteRenderer>();
        _uiman = GameObject.FindWithTag("UIman").GetComponent<UIman>();
        _gameman = GameObject.FindWithTag("Gameman").GetComponent<Game_man>();
        _audio = GetComponentInChildren<Character_audio>();
    }
    void Update()
    {
        OutOfMap();
        if (_canWalk == true)
        {
            Movement();
        }
    }
    void Movement()
    {
        //WALK
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Flip(horizontalInput);
        _rigid.velocity = new Vector2(horizontalInput * _speed, _rigid.velocity.y);
        _playerAnim.RunAnim(horizontalInput);
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && GroundCalculate() == true && _duringRoll == false)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _playerAnim.JumpAnim();
            StartCoroutine(ResetingJump());
        }
        //FAST FALL
        if (Input.GetKeyDown(KeyCode.S) && GroundCalculate() == false)
        {
            _playerAnim.ClimbAnimStop();
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce * -1);
        }
        if (Input.GetKeyDown(KeyCode.W) && _canClimb == true)
        {
            _rigid.gravityScale = 0;
            _playerAnim.ClimbAnimStart();
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce / 1.5f);
        }
        //ROLL
        if (Input.GetKeyDown(KeyCode.LeftShift) && _canRoll == 0 && _attacking == false)
        {
            _canRoll = 1;
            _playerAnim.RollAnim();
            _audio.DashAudio();
            StartCoroutine(Rolling());
        }
        //SWING
        if (Input.GetKeyDown(KeyCode.K) && _attacking == false && _duringRoll == false)
        {
            StartCoroutine(Attack());
            _audio.SwingAudio();
        }
        //KEY
        if (Input.GetKeyDown(KeyCode.J) && _keyFound == true)
        {
            SpawnKey();
        }
    }
    //CAN WE JUMP?
    bool GroundCalculate()
    {
        int layerMask = (1 << 6) | (1 << 7);
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(transform.position.x - 0.3f, transform.position.y), Vector2.down, 0.3f, layerMask);
        RaycastHit2D hitInfo1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.3f, transform.position.y), Vector2.down, 0.3f, layerMask);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 0.3f, layerMask);
        if (hitInfo.collider != null || hitInfo1.collider != null || hitInfo2.collider != null)
        {
            if (_resetJump == false)
            {
                return true;
            }
        }
        return false;
    }
    //WHEN CAN WE JUMP AGAIN?
    IEnumerator ResetingJump()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
    //FACING LEFT OR RIGHT?
    void Flip(float move)
    {
        if (move > 0)
        {
            _playerSprite.flipX = false;
            _facedRight = true;
        }
        else if (move < 0)
        {
            _playerSprite.flipX = true;
            _facedRight = false;
        }
    }
    IEnumerator Rolling()
    {
        _duringRoll = true;
        _playerDamageable = false;
        _speed = _speed * 1.5f;
        yield return new WaitForSeconds(1.0f);
        _speed = _speed / 1.5f;
        _playerDamageable = true;
        _duringRoll = false;
        yield return new WaitForSeconds(2.5f);
        _canRoll = 0;
    }
    IEnumerator Attack()
    {
        _attacking = true;
        if (_facedRight == true)
        {
            _playerAnim.SwingRightAnim();
        }
        else if (_facedRight == false)
        {
            _playerAnim.SwingLeftAnim();
        }
        yield return new WaitForSeconds(_attackDuration);
        _attacking = false;
    }
    public void TakeDamage()
    {
        if (_playerDamageable == true)
        {
            _playerAnim.Damaged();
            _health = _health - 1;
            _uiman.DamageUpdate(_health);
            if (_health > 0)
            {
                _audio.DamageAudio();
                StartCoroutine(Stun(_stunDuration));
            }
            else if (_health == 0)
            {
                _audio.DeathAudio();
                StopAllCoroutines();
                _canWalk = false;
                _playerDamageable = false;
                _spawnman.StopSpawn();
                _playerAnim.DeathAnim();
                _uiman.GameOverSequence();
                _gameman.GameOver();
            }
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
        _canClimb = false;
        _rigid.gravityScale = 1;
        _playerAnim.ClimbAnimStop();
    }
    public void EnableKey()
    {
        _keyFound = true;
    }
    protected void SpawnKey()
    {
        _keyFound = false;
        _playerkey.enabled = false;
        if (_facedRight == true)
        {
            Instantiate(_key, new Vector2(transform.position.x + 0.8f, transform.position.y + 0.3f), Quaternion.identity);
        }
        else if (_facedRight == false)
        {
            Instantiate(_key, new Vector2(transform.position.x - 0.8f, transform.position.y + 0.3f), Quaternion.identity);
        }
    }
    //Win check
    protected void OutOfMap()
    {
        if (transform.position.y < -8)
        {
            _playerDamageable = false;
            _spawnman.StopSpawn();
            _uiman.GameWinSequence();
            _gameman.GameOver();
        }
    }
}
