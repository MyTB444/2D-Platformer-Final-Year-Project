using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Game_man _gameman;
    private Collider2D _collider;
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _gameman = GameObject.FindWithTag("Gameman").GetComponent<Game_man>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key")
        {
            Destroy(other.gameObject);
            _gameman.DestroyGate();
        }
    }
}
