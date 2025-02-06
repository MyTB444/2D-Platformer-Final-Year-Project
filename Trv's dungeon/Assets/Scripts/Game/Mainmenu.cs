using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI highestScore;
    [SerializeField] private Animator _menu;
    void Start()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        highestScore.text = FormatTime(bestTime);
        _audioSource = GetComponent<AudioSource>();
    }
    public void StartButton()
    {
        StartCoroutine(GameStarting());
        _audioSource.Play();
    }
    public void ExitButton()
    {
        _audioSource.Play();
        Application.Quit();
    }
    //Play menu animation and delay game load.
    private IEnumerator GameStarting()
    {
        _menu.SetTrigger("Start");
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Game");
    }
    string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);
        return $"{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
    }
}
