using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Update()
    {
        Flow();
    }
    private void Flow()
    {
        gameObject.transform.Translate(new Vector2(1, 0) * 0.003f);
        if (transform.position.x >= 58.3f)
        {
            transform.position = new Vector2(-58.3f, 0);
        }
    }

}
