using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform _archer;
    void Start()
    {
        _archer = transform.parent.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_archer.position.x < transform.position.x)
        {
            transform.Translate(new Vector2(6, 0) * Time.deltaTime);
        }
        else if (_archer.position.x > transform.position.x)
        {
            transform.Translate(new Vector2(-6, 0) * Time.deltaTime);
        }
    }
}
