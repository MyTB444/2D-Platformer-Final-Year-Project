using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _attackDuration;
    [SerializeField] protected float _stunDuration;
    [SerializeField] protected int _health;
    public float _playerfragility;
    private float horizontalInput;
    private int _canRoll = 0;
    private bool _canWalk = true;
    protected bool _canClimb;
    private bool _facedRight;
    private bool _duringRoll;
    private bool _resetJump = false;
    private bool _attacking = false;
    private bool _keyFound;

    //Components
    [SerializeField] private GameObject _key;
    private SpriteRenderer _playerkey;
    private SpriteRenderer _playerSprite;
    private PlayerAnimation _playerAnim;
    private Rigidbody2D _rigid;
    private UIman _uiman;
    private Game_man _gameman;
    // Start is called before the first frame update
    void Start()
    {
        _keyFound = false;
        //HANDLES
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _playerkey = GameObject.FindWithTag("Playerkey").GetComponent<SpriteRenderer>();
        _uiman = GameObject.FindWithTag("UIman").GetComponent<UIman>();
        _gameman = GameObject.FindWithTag("Gameman").GetComponent<Game_man>();
    }

    // Update is called once per frame
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Flip(horizontalInput);
        _rigid.velocity = new Vector2(horizontalInput * _speed, _rigid.velocity.y);
        _playerAnim.RunAnim(horizontalInput);
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && GroundCalculate() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _playerAnim.JumpAnim();
            StartCoroutine(ResetingJump());
        }
        //FAST FALL
        if (Input.GetKeyDown(KeyCode.S) && GroundCalculate() == false)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce * -1);
        }
        if (Input.GetKeyDown(KeyCode.W) && _canClimb == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce / 1.5f);
            _playerAnim.ClimbAnim();
        }
        //ROLL
        if (Input.GetKeyDown(KeyCode.LeftShift) && GroundCalculate() == true && _canRoll == 0)
        {
            _canRoll = 1;
            _playerAnim.RollAnim();
            StartCoroutine(Rolling());
        }
        //SWING
        if (Input.GetKeyDown(KeyCode.K) && _attacking == false && _duringRoll == false || Input.GetKeyDown(KeyCode.K) && _attacking == false && GroundCalculate() == false)
        {
            StartCoroutine(Attack());
        }
        //KEY
        if (Input.GetKeyDown(KeyCode.J) && _keyFound == true)
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

    }
    //CAN WE JUMP?
    bool GroundCalculate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(transform.position.x - 0.1f, transform.position.y), Vector2.down, 0.3f, 1 << 6);
        RaycastHit2D hitInfo1 = Physics2D.Raycast(new Vector2(transform.position.x + 0.1f, transform.position.y), Vector2.down, 0.3f, 1 << 6);
        if (hitInfo.collider != null || hitInfo1.collider != null)
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
        _speed = _speed * 2;
        yield return new WaitForSeconds(1.0f);
        _speed = _speed / 2;
        _duringRoll = false;
        yield return new WaitForSeconds(1.5f);
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
        _canWalk = false;
        _uiman.DamageUpdate(_health);
        _health = _health - 1;
        StartCoroutine(Stun(_stunDuration));
        if (_health <= 0)
        {
            Destroy(this.gameObject);
            _uiman.GameOverSequence();
            _gameman.GameOver();
        }

    }
    public IEnumerator Stun(float time)
    {
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
    }
    public void EnableKey()
    {
        _keyFound = true;
    }
    protected void OutOfMap()
    {
        if (transform.position.y < -8)
        {
            _uiman.GameWinSequence();
            _gameman.GameOver();
        }
    }


}
