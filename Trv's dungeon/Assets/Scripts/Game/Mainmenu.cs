using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] private Animator _menu;
    public void StartButton()
    {
        StartCoroutine(GameStarting());
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    private IEnumerator GameStarting()
    {
        _menu.SetTrigger("Start");
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("Game");
    }
}
