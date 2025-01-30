using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bossfightdetector : MonoBehaviour
{
    [SerializeField] UnityEvent bossfight;
    int x = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (x == 0)
            {
                x++;
                bossfight.Invoke();
            }
        }
    }
}
