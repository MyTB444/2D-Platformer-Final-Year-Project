using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _velocity = 8.0f;
    Vector3 yon = new Vector3(0, 2, 0);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(yon * _velocity * Time.deltaTime);
        if (transform.position.y >= 10)
        {
            Destroy(this.gameObject);
        }
    }
}
