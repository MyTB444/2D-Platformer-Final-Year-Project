using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral_hitbox : MonoBehaviour
{
    private int collisionnumber = 0;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionnumber < 1)
        {
            if (other.gameObject.tag == "Enemy")
            {
                collisionnumber++;
                Enemy _enemyHit = other.gameObject.GetComponentInParent<Enemy>();
                Rigidbody2D _enemyRigid = other.gameObject.GetComponent<Rigidbody2D>();
                Transform _enemyTransform = other.gameObject.GetComponent<Transform>();
                if (_enemyTransform.position.x > transform.position.x)
                {
                    _enemyHit.TakeDamage();
                    _enemyRigid.velocity = new Vector2(_enemyHit._enemyFragility, _enemyHit._enemyFragility);
                }
                else if (_enemyTransform.position.x < transform.position.x)
                {
                    _enemyHit.TakeDamage();
                    _enemyRigid.velocity = new Vector2(_enemyHit._enemyFragility * -1, _enemyHit._enemyFragility);
                }
            }
            else if (other.gameObject.tag == "Player")
            {
                collisionnumber++;
                Player _playerHit = other.gameObject.GetComponentInParent<Player>();
                Rigidbody2D _playerRigid = other.gameObject.GetComponent<Rigidbody2D>();
                Transform _playerTransform = other.gameObject.GetComponent<Transform>();
                _playerHit.TakeDamage();
                if (_playerTransform.position.x > transform.position.x)
                {
                    _playerRigid.velocity = new Vector2(_playerHit._playerfragility, _playerHit._playerfragility);
                }
                else if (_playerTransform.position.x < transform.position.x)
                {
                    _playerRigid.velocity = new Vector2(_playerHit._playerfragility * -1, _playerHit._playerfragility);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        collisionnumber = 0;
    }
    private void Update()
    {
        if (transform.position.y <= -30)
        {
            Destroy(this.gameObject);
        }
    }
}