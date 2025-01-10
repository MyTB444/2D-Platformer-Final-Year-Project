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
    private Rigidbody2D rb;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = _player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y + 0.2f).normalized * _speed;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
}
