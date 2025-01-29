using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Arrow : MonoBehaviour
{
    // Check parent object location and move accordingly.
    private Transform _player;
    public float _speed;
    [SerializeField] private float rotation;
    [SerializeField] private float aim;
    private Rigidbody2D rb;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = _player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y + aim).normalized * _speed;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + rotation);
    }
    // void Update()
    //{
    //  if (transform.position.y > 30 || transform.position.y < -50 || transform.position.x < -60 || transform.position.x > 60)
    //{
    //  Destroy(this.gameObject);
    //}
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (this.gameObject.tag == "Arrow")
            {
                Destroy(this.gameObject);
            }
        }
    }
}
