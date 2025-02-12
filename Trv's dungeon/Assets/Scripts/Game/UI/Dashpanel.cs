using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dashpanel : MonoBehaviour
{
    private float remaining;
    private float max = 3f;
    public Slider slider;
    void Update()
    {
        slider.value = CalculateSlider();
        if (remaining <= 0)
        {
            remaining = 0;
        }
        else if (remaining > 0)
        {
            remaining -= Time.deltaTime;
        }
    }
    float CalculateSlider()
    {
        return remaining / max;
    }
    public void StartTimer()
    {
        remaining = max;
    }
}
