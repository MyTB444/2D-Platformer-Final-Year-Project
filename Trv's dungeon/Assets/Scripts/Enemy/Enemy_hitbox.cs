using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_hitbox : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player _playerHit = other.gameObject.GetComponentInParent<Player>();
            Rigidbody2D _playerRigid = other.gameObject.GetComponent<Rigidbody2D>();
            Transform _playerTransform = other.gameObject.GetComponent<Transform>();
            if (_playerTransform.position.x > transform.position.x)
            {
                _playerHit.TakeDamage();
                _playerRigid.velocity = new Vector2(_playerHit._playerfragility, _playerHit._playerfragility);
            }
            else if (_playerTransform.position.x < transform.position.x)
            {
                _playerHit.TakeDamage();
                _playerRigid.velocity = new Vector2(_playerHit._playerfragility * -1, _playerHit._playerfragility);
            }

        }
        if (this.gameObject.tag == "Arrow" && other.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
