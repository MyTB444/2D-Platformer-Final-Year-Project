using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform[] movepoints;
    [SerializeField] private int speed;
    private int _currentSpeed;
    private Rigidbody2D rb;
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        Movement();
    }
    private void Movement()
    {
        if (transform.position.x <= movepoints[0].position.x)
        {
            _currentSpeed = speed;
        }
        else if (transform.position.x >= movepoints[1].position.x)
        {
            _currentSpeed = speed * -1;
        }
        rb.velocity = new Vector2(_currentSpeed, rb.velocity.y);
    }
}
