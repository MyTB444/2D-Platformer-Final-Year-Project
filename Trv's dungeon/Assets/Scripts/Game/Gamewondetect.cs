using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gamewondetect : MonoBehaviour
{
    [SerializeField] UnityEvent gamewon;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gamewon.Invoke();
        }
    }
}
