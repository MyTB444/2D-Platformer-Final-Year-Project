using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Enable/disable player climb.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player _player = other.gameObject.GetComponent<Player>();
            _player.EnableCLimb();

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player _player = other.gameObject.GetComponent<Player>();
            _player.DisableClimb();
        }
    }
}
