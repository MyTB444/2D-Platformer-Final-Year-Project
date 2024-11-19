using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _attackDuration;
    private int _canRoll = 0;
    private bool _facedRight;
    private bool _duringRoll;
    private bool _resetJump = false;
    private bool _attacking = false;

    //Components
    [SerializeField] private GameObject _swingHitbox;
    private SpriteRenderer _playerSprite;
    private PlayerAnimation _playerAnim;
    private Rigidbody2D _rigid;
    // Start is called before the first frame update
    void Start()
    {
        //HANDLES
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Flip(horizontalInput);
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
        //ROLL
        if (Input.GetKeyDown(KeyCode.LeftShift) && GroundCalculate() == true && _canRoll == 0)
        {
            _canRoll = 1;
            _playerAnim.RollAnim();
            StartCoroutine(Rolling());
        }
        //RUN
        _rigid.velocity = new Vector2(horizontalInput * _speed, _rigid.velocity.y);
        _playerAnim.RunAnim(horizontalInput);
        //SWING
        if (Input.GetKeyDown(KeyCode.K) && _attacking == false && _duringRoll == false || Input.GetKeyDown(KeyCode.K) && _attacking == false && GroundCalculate() == false)
        {
            StartCoroutine(Attack());
        }

    }
    //CAN WE JUMP?
    bool GroundCalculate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, 1 << 6);
        if (hitInfo.collider != null)
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

}
