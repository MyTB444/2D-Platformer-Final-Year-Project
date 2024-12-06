using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player_hitbox : MonoBehaviour
{
    private SpriteRenderer _playerkey;
    private Player _player;
    private BoxCollider2D _playerHitbox;
    private int collisionnumber;
    private Game_man _gameman;
    void Start()
    {
        _player = GetComponentInParent<Player>();
        _playerHitbox = GetComponent<BoxCollider2D>();
        _gameman = GameObject.FindGameObjectWithTag("Gameman").GetComponent<Game_man>();
        _playerkey = GameObject.FindWithTag("Playerkey").GetComponent<SpriteRenderer>();
    }
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

            else if (other.gameObject.name == "Diamond")
            {
                Destroy(other.gameObject);
                _gameman.DiamonFound();
            }
            else if (other.gameObject.name == "Hearth")
            {
                Destroy(other.gameObject);
                _gameman.HearthFound();
            }
            else if (other.gameObject.tag == "Key")
            {
                Destroy(other.gameObject);
                _playerkey.enabled = true;
                _player.EnableKey();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        collisionnumber = 0;

    }
}

