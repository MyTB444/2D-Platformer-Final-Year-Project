using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player_hitbox : MonoBehaviour
{
    private int collisionnumber;
    public UIman ui;
    // Damage and push enemies with handles
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
            else if (other.gameObject.tag == "Startgate")
            {
                collisionnumber++;
                ui.StartTimer();
                Animator gate = other.gameObject.GetComponent<Animator>();
                gate.SetTrigger("Play");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(HitboxColdown());
    }
    private IEnumerator HitboxColdown()
    {
        yield return new WaitForSeconds(0.4f);
        collisionnumber = 0;
    }
}

