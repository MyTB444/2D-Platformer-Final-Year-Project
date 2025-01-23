using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_input : MonoBehaviour
{
    private float horizontalInput;
    private Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
    }
    void Update()
    {
        if (_player._canWalk == true)
        {
            MovementInput();
        }
    }
    void MovementInput()
    {
        //WALK
        horizontalInput = Input.GetAxisRaw("Horizontal");
        _player.Walk(horizontalInput);
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.Jump();
        }
        //FAST FALL
        if (Input.GetKeyDown(KeyCode.S))
        {
            _player.FastFall();
        }
        //CLIMB
        if (Input.GetKeyDown(KeyCode.W))
        {
            _player.Climb();
        }
        //ROLL
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _player.StartRoll();
        }
        //SWING
        if (Input.GetKeyDown(KeyCode.K))
        {
            _player.StartAttack();
        }
        //KEY
        if (Input.GetKeyDown(KeyCode.J))
        {
            _player.DropKey();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _player.StartPickUp();
        }
    }
}
