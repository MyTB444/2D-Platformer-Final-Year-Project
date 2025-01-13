using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Background image manual animation.
    void Update()
    {
        Flow();
    }
    private void Flow()
    {
        gameObject.transform.Translate(new Vector2(0.5f, 0) * 0.006f);
        if (transform.position.x >= 110.36f)
        {
            transform.position = new Vector2(-110.36f, 0);
        }
    }

}
