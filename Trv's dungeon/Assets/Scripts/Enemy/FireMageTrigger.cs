using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class FireMageTrigger : MonoBehaviour
{
    public Firemage fm;
    private int x = 0;
   public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && x == 0)
        {
            fm.StartThings();
            x ++;
        }
    }
}
