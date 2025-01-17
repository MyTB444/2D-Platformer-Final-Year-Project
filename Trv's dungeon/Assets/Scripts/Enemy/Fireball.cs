using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Firemage fm;
    private SpriteRenderer sr;
    [SerializeField] private float speed;
    void Start()
    {
        fm = GetComponentInParent<Firemage>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (fm._facedRight == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            sr.flipX = false;
        }
        else if (fm._facedRight == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
            sr.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 60)
        {
            Destroy(this.gameObject);
        }
    }
}
