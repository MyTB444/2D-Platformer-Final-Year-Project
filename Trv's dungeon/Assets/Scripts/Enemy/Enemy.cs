using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;
    [SerializeField] protected float _jumpCooldown;
    [SerializeField] protected bool _canJump;
    protected Transform _target;
    protected Rigidbody2D _rigid;

    protected void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rigid = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public abstract void Update();
    public void Movement()
    {
        if (_target.position.x < transform.position.x)
        {
            _rigid.velocity = new Vector2(_speed * -1, _rigid.velocity.y);
        }
        else if (_target.position.x > transform.position.x)
        {
            _rigid.velocity = new Vector2(_speed, _rigid.velocity.y);
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

}
