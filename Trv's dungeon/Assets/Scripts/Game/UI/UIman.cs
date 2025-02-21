using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIman : MonoBehaviour
{
    public bool _gameStop;
    private float startTime = 0;
    public TextMeshProUGUI timeText;
    public Slider slider;
    public float scoreTime = 0;
    [SerializeField] private Animator[] _live;
    public Animator[] waveText;
    public TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI _gameoverText;
    [SerializeField] private GameObject _gamewinText;
    // Play hearth sign animations for taking damage.
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Endless")
        {
            startTime = Time.time;
        }
    }
    public void StartTimer()
    {
        startTime = Time.time;
        _gameStop = false;
    }
    public void DamageUpdate(int i)
    {
        _live[i].SetTrigger("Destroy");
    }
    //Activate gameover text.
    public void GameOverSequence()
    {
        _gameStop = true;
        slider.gameObject.SetActive(false);
        _gameoverText.gameObject.SetActive(true);
    }
    public void EndlessGameOver()
    {
        scoreTime = Time.time - startTime;
        SaveBestWave(scoreTime);
        finalScore.text = FormatTime(scoreTime);
        slider.gameObject.SetActive(false);
        finalScore.gameObject.SetActive(true);
        timeText.gameObject.SetActive(false);
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
    // Format time 
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
            PlayerPrefs.SetInt("SetTime", 5);
            PlayerPrefs.Save();
        }
    }
    void SaveBestWave(float time)
    {
        float bestTime = PlayerPrefs.GetFloat("BestWave", float.MinValue);
        if (time > bestTime)
        {
            PlayerPrefs.SetFloat("BestWave", time);
            PlayerPrefs.SetInt("SetWave", 5);
            PlayerPrefs.Save();
        }

    }
    // Regen player hp
    public void Regen()
    {
        for (int i = 0; i < 5; i++)
        {
            _live[i].SetTrigger("Regen");
        }
    }
    public void WaveTextPlay(int x)
    {
        waveText[x].SetTrigger("Play");
    }

}
