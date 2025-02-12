using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIman : MonoBehaviour
{
    private bool _gameStop = false;
    private float startTime = 0;
    public TextMeshProUGUI timeText;
    public float scoreTime = 0;
    [SerializeField] private Animator[] _live;
    public TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI _gameoverText;
    [SerializeField] private TextMeshProUGUI _gamewinText;
    // Play hearth sign animations for taking damage.
    private void Start()
    {
        startTime = Time.time;
    }
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
        scoreTime = Time.time - startTime;
        SaveBestTime(scoreTime);
        finalScore.text = FormatTime(scoreTime);
        _gamewinText.gameObject.SetActive(true);
        finalScore.gameObject.SetActive(true);
        timeText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (_gameStop == false)
        {
            DisplayTime(Time.time - startTime);
        }
    }
    string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);
        return $"{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
    }

    // Displays the current time in the UI
    void DisplayTime(float time)
    {
        string formattedTime = FormatTime(time);
        if (timeText != null)
            timeText.text = formattedTime;
    }
    void SaveBestTime(float time)
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (time < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", time);
            PlayerPrefs.Save();
        }
    }
    //Display time tracker.
    public void Regen()
    {
        for (int i = 0; i < 5; i++)
        {
            _live[i].SetTrigger("Regen");
        }
    }
}
