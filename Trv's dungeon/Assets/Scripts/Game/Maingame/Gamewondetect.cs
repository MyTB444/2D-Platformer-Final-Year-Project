using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gamewondetect : MonoBehaviour
{
    // Observant for win
    [SerializeField] UnityEvent gamewon;
    private int x = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && x == 0)
        {
            x++;
            gamewon.Invoke();
        }
    }
}
