using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIman : MonoBehaviour
{

    public float timeValue = 0;
    private bool _gameStop = false;
    public TextMeshProUGUI TimeText;
    [SerializeField] private Animator[] _live;
    public TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI _gameoverText;
    [SerializeField] private TextMeshProUGUI _gamewinText;
    // Play hearth sign animations for taking damage.
    public void DamageUpdate(int i)
    {
        _live[i].SetTrigger("Destroy");
    }
    //Activate gameover text.
    public void GameOverSequence()
    {
        _gameStop = true;
        _gameoverText.gameObject.SetActive(true);
    }
    //Activate game win tests.
    public void GameWinSequence()
    {
        _gameStop = true;
        _gamewinText.gameObject.SetActive(true);
        finalScore.gameObject.SetActive(true);
        TimeText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_gameStop == false)
        {
            timeValue += Time.deltaTime;
        }
        DisplayTime(timeValue);
    }
    //Display time tracker.
    private void DisplayTime(float timeToDisplay)
    {
        float Minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float Seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float MilleSeconds = timeToDisplay % 1 * 1000;

        TimeText.text = string.Format("{0:0}:{1:00}:{2:00}", Minutes, Seconds, MilleSeconds);
        finalScore.text = string.Format("{0:0}:{1:00}:{2:00}", Minutes, Seconds, MilleSeconds);
    }
}
