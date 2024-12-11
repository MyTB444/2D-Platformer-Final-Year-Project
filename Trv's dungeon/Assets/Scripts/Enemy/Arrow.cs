using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Arrow : MonoBehaviour
{
    // Check parent object location and move accordingly.
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
            transform.Translate(new Vector2(9, 0) * Time.deltaTime);
        }
        else if (_archer.position.x > transform.position.x)
        {
            transform.Translate(new Vector2(-9, 0) * Time.deltaTime);
        }
    }
}
