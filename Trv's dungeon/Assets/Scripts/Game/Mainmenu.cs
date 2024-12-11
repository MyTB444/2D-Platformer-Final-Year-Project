using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private Animator _menu;
    void Start()
    {
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
}
