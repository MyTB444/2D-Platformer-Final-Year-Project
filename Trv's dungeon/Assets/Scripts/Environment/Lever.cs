using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Rigidbody2D gate;
    [SerializeField] private Sprite[] sprites;
    private bool mooving = false;
    private SpriteRenderer sr;
    [SerializeField] private float state;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void MoveGates()
    {
        if (mooving == false)
        {
            mooving = true;
            StartCoroutine(MovingGates());
        }
    }
    IEnumerator MovingGates()
    {
        ChangeSprite();
        gate.velocity = Vector2.up * state;
        yield return new WaitForSeconds(2f);
        gate.velocity = Vector2.zero;
        state = state * -1;
        mooving = false;
    }
    private void ChangeSprite()
    {
        if (sr.sprite == sprites[0])
        {
            sr.sprite = sprites[1];
        }
        else if (sr.sprite == sprites[1])
        {
            sr.sprite = sprites[0];
        }
    }
}
