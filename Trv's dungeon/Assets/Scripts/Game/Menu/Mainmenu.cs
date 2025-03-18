using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip _text6sound;
    public AudioClip _text2sound;
    public AudioClip oldClick;
    public AudioClip newClick;

    [SerializeField] private TextMeshProUGUI highestScore;
    [SerializeField] private TextMeshProUGUI highestScore2;
    public TextMeshProUGUI begintext;
    public GameObject begintextObj;
    public GameObject trv;
    public GameObject facts;
    public GameObject controls;
    public GameObject main;
    public Animator menu2;
    [SerializeField] private Animator _menu;
    void Start()
    {
        // Display highest score
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        float bestWave = PlayerPrefs.GetFloat("BestWave", float.MaxValue);
        int set2 = PlayerPrefs.GetInt("SetWave");
        int set = PlayerPrefs.GetInt("SetTime");
        if (set == 5)
        {
            highestScore.text = FormatTime(bestTime);
        }
        if (set2 == 5)
        {
            highestScore2.text = FormatTime(bestWave);

        }

        _audioSource = GetComponent<AudioSource>();
    }
    public void StartButton()
    {
        StartCoroutine(GameStarting());
        _audioSource.clip = newClick;
        _audioSource.Play();
    }
    public void EndlessButton()
    {
        StartCoroutine(EndlessStarting());
        _audioSource.clip = newClick;
        _audioSource.Play();
    }
    public void ExitButton()
    {
        _audioSource.clip = oldClick;
        _audioSource.Play();
        Application.Quit();
    }
    public void EnableFacts()
    {
        _audioSource.clip = oldClick;
        _audioSource.Play();
        main.SetActive(false);
        facts.SetActive(true);
    }
    public void DisableFacts()
    {
        _audioSource.clip = oldClick;
        _audioSource.Play();
        main.SetActive(true);
        facts.SetActive(false);
    }
    public void EnableControls()
    {
        _audioSource.clip = oldClick;
        _audioSource.Play();
        main.SetActive(false);
        controls.SetActive(true);
    }
    public void DisableControls()
    {
        _audioSource.clip = oldClick;
        _audioSource.Play();
        main.SetActive(true);
        controls.SetActive(false);
    }
    //Play menu animation and delay game load.
    private IEnumerator GameStarting()
    {
        _menu.SetTrigger("Start");
        menu2.SetTrigger("Start2");
        yield return new WaitForSeconds(4.0f);
        trv.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(StartText());
        yield return new WaitForSeconds(11.0f);
        SceneManager.LoadScene("Game");
    }
    private IEnumerator StartText()
    {
        string fullText = "The Pope is closed in the dungeons deep. Seek thee a path unto him, yet tread with wary step—for thou shalt perish but once.";
        begintextObj.SetActive(true);
        _audioSource.clip = _text6sound;
        _audioSource.Play();
        foreach (char letter in fullText)
        {
            begintext.text += letter;
            yield return new WaitForSeconds(0.046f);
        }
        string remText = "And yeah... be quick, MAKE HASTE!";
        yield return new WaitForSeconds(1.0f);
        _audioSource.clip = _text2sound;
        _audioSource.Play();
        foreach (char letter in remText)
        {
            begintext.text += letter;
            yield return new WaitForSeconds(0.055f);
        }
    }
    private IEnumerator EndlessStarting()
    {
        _menu.SetTrigger("Start");
        menu2.SetTrigger("Start2");
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Endless");
    }
    // Format time from float to time with miliseconds
    string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);
        return $"{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
    }
}
