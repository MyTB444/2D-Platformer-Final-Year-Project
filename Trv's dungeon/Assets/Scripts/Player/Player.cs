using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
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
    public bool canDoubleJump = false;
    private bool airjump;
    [SerializeField] private int jumpCount = 0;
    private int rollState = 0;
    private bool canPickUp = true;
    private bool canAttack = true;
    [SerializeField] private bool hasSword;
    public bool _canWalk = true;
    protected bool _canClimb;
    //Component variables for handles
    [SerializeField] private GameObject _key;
    private Character_audio _audio;
    [SerializeField] private GameObject wavePrefab;
    private SpriteRenderer _playerSprite;
    [SerializeField] UnityEvent playerIsDead;
    private PlayerAnimation _playerAnim;
    private Rigidbody2D _rigid;
    private UIman _uiman;
    void Start()
    {
        //HANDLES
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _uiman = GameObject.FindWithTag("UIman").GetComponent<UIman>();
        _audio = GetComponentInChildren<Character_audio>();
    }
    void Update()
    {
        GroundCalculate();
        SlowCheck();
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
        int layerMask = (1 << 6) | (1 << 7) | (1 << 8);
        RaycastHit2D groundInfo = Physics2D.BoxCast(transform.position, new Vector2(0.6f, 0.2f), 0.0f, Vector2.down, 0.3f, layerMask);
        if (groundInfo.collider != null)
        {
            currentAirState = AirState.Grounded;
            airjump = false;
        }
        else
        {
            currentAirState = AirState.InAir;
        }
    }
    private void SlowCheck()
    {
        if (currentMovementState != MovementState.Rolling)
        {
            RaycastHit2D groundInfo1 = Physics2D.BoxCast(transform.position, new Vector2(0.6f, 0.2f), 0.0f, Vector2.down, 0.4f, 1 << 8);
            if (groundInfo1.collider != null)
            {
                _jumpForce = 4.6f;
                _speed = 1;
            }
            else if (groundInfo1.collider == null)
            {
                _jumpForce = 8;
                _speed = 5.5f;
            }
        }
    }

    public void Jump()
    {
        if (currentAirState == AirState.Grounded && currentMovementState == MovementState.Standing)
        {
            _playerAnim.JumpAnim();
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            jumpCount++;
        }
        else if (canDoubleJump == true && jumpCount > 0 && currentMovementState == MovementState.Standing)
        {
            _playerAnim.JumpAnim();
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            jumpCount = 0;
            airjump = true;
        }
        else if (canDoubleJump == true && currentAirState == AirState.InAir && airjump == false)
        {
            _playerAnim.JumpAnim();
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            airjump = true;
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
        if (currentMovementState == MovementState.Standing && canAttack == true)
        {
            canAttack = false;
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
            if (hasSword == true)
            {
                _playerAnim.BlueRight();
            }
        }
        else if (FacedRight() == false)
        {
            _playerAnim.SwingLeftAnim();
            if (hasSword == true)
            {
                _playerAnim.BlueLeft();
            }
        }
        yield return new WaitForSeconds(0.3f);
        currentMovementState = MovementState.Standing;
        yield return new WaitForSeconds(_attackDuration);
        canAttack = true;
    }
    public void GotSword()
    {
        hasSword = true;
    }
    public void StartPickUp()
    {
        StartCoroutine(PickUp());
    }
    IEnumerator PickUp()
    {
        if (currentMovementState == MovementState.Standing)
        {
            if (canPickUp == true)
            {
                canPickUp = false;
                _audio.SwingAudio();
                if (FacedRight() == true)
                {
                    _playerAnim.PickUpRight();
                }
                else if (FacedRight() == false)
                {
                    _playerAnim.PickUpLeft();
                }
                yield return new WaitForSeconds(0.3f);
                canPickUp = true;
            }
        }
    }
    public void TakeDamage()
    {
        if (currentMovementState != MovementState.Rolling && IsPlayerDead() == false)
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
    public void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    public void DisableDoubleJump()
    {
        canDoubleJump = false;
    }
    public void DropItem(int index)
    {
        InventorySystem inventory = FindObjectOfType<InventorySystem>();
        InventoryItemSO itemToDrop = inventory.GetItem(index);

        Vector3 dropPosition = transform.position + transform.forward * 1.5f + Vector3.up * 0.5f;
        GameObject droppedItem = Instantiate(_key, dropPosition, Quaternion.identity);

        droppedItem.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        Player_pickbox droppedItemPickup = droppedItem.GetComponent<Player_pickbox>();
        if (droppedItemPickup != null)
        {
            droppedItemPickup.key = itemToDrop;
        }
        inventory.RemoveItem(index);
    }
    public void DropKey()
    {
        InventorySystem inventory = FindObjectOfType<InventorySystem>();

        for (int i = 0; i < 3; i++)
        {
            InventoryItemSO item = inventory.GetItem(i);
            if (item != null && item.isKey)
            {
                DropItem(i);
                break;
            }
        }
    }
    public void Regen()
    {
        _health = 5;
        _uiman.Regen();
    }
}


