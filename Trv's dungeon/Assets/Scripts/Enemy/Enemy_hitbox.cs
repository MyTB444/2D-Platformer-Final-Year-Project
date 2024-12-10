using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_hitbox : MonoBehaviour
{
    private int collisionnumber;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionnumber < 1)
        {
            if (other.gameObject.tag == "Player")
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
            if (this.gameObject.tag == "Arrow" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Ladder" && other.gameObject.tag != "Arrow")
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        collisionnumber = 0;
    }
}
