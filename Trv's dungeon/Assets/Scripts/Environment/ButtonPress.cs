using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Sprite[] srs;
    [SerializeField] private GameObject rockprefab;
    private BoxCollider2D bcollider;
    private int presscount;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bcollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsPressed();
    }
    private void IsPressed()
    {
        RaycastHit2D upInfo1; // = Physics2D.Raycast(transform.position, Vector2.up, 1 << 3);
        upInfo1 = Physics2D.BoxCast(transform.position, new Vector2(0.25f, 0.2f), 0f, new Vector2(0, 1f), 0.15f, 1 << 3);
        Debug.DrawRay(transform.position, Vector2.up, Color.green);
        if (presscount == 0)
        {
            if (upInfo1.collider != null)
            {
                StartCoroutine(ButtonPressed());
            }

            else if (upInfo1.collider == null)
            {
                sr.sprite = srs[0];
            }
        }

    }
    IEnumerator ButtonPressed()
    {
        presscount = 1;
        sr.sprite = srs[1];
        bcollider.offset = new Vector2(0, -0.06f);
        SpawnRock();
        yield return new WaitForSeconds(5.0f);
        bcollider.offset = new Vector2(0, -0.04f);
        presscount = 0;
    }
    private void SpawnRock()
    {
        Instantiate(rockprefab, new Vector2(transform.position.x, transform.position.y + 16f), Quaternion.identity, gameObject.transform);
    }
}
