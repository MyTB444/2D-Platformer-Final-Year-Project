using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed = 30f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Transform parentTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        parentTransform = transform.parent;
        StartCoroutine(Destroy());
    }

    void FixedUpdate()
    {
        float direction = transform.position.x > parentTransform.position.x ? 1f : -1f;
        spriteRenderer.flipX = direction < 0;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
