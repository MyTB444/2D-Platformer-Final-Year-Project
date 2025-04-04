using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dashpanel : MonoBehaviour
{

    private float max = 3.1f;
    public Slider slider;
    void Start()
    {
        slider.value = 1f;
    }
    public void StartTimer()
    {
        StartCoroutine(DashCooldownRoutine());
    }
    //fill the slider in 3.1f time (dash cooldown)
    IEnumerator DashCooldownRoutine()
    {
        slider.value = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < max)
        {
            slider.value = elapsedTime / max;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = 1f;
    }
}
