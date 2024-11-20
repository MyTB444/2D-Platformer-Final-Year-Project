using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player_hitbox : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
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
    }

}
