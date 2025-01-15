using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_hitbox : MonoBehaviour
{
    // Assing handles to the player if we hit the player. Call its take damage, and push it away.
    [SerializeField] private int collisionnumber = 0;
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
        }
    }
    void Update()
    {
        if (collisionnumber == 1)
        {
            StartCoroutine(ResetCollision());
        }
    }
    private IEnumerator ResetCollision()
    {
        yield return new WaitForSeconds(0.7f);
        collisionnumber = 0;
    }
}
