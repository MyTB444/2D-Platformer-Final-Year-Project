using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_man : MonoBehaviour
{
    private bool _isGameover = false;
    public float startDelay;
    private Collider2D _gateCollider;
    private Audioman _audio;
    public GameObject cam;
    [SerializeField] private GameObject[] firewall;
    [SerializeField] private GameObject _gate;
    [SerializeField] private Animator pop;
    [SerializeField] private GameObject _diamond;
    [SerializeField] private GameObject _emerald;
    [SerializeField] private GameObject _gem;
    [SerializeField] private int _count;
    void Start()
    {
        StartCoroutine(StartDelay());
        _audio = GameObject.FindWithTag("Audioman").GetComponent<Audioman>();
        if (GameObject.FindWithTag("Gate") != null)
        {
            _gateCollider = GameObject.FindWithTag("Gate").GetComponent<Collider2D>();
        }
        _count = 0;
    }
    // Delay the follow camera when the game begins
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        cam.SetActive(true);
    }
    //Restart and exit buttons.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameover == true || Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                SceneManager.LoadScene("Endless");
            }
        }
        if (Input.GetKeyDown(KeyCode.H) && _isGameover == true)
        {
            SceneManager.LoadScene("Mainmenu");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("Mainmenu");
        }
    }
    public void DestroyGate()
    {
        _audio.CLickAudio();
        Destroy(_gate.gameObject);
    }
    // If gem, everald or diamond is found, inform related objects.
    public void GemFound()
    {
        _count = _count + 1;
        _audio.GlassB();
        Destroy(_gem.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    public void DiamondFound()
    {
        _count = _count + 1;
        _audio.GlassB();
        Destroy(_diamond.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    public void EmeraldFound()
    {
        _count = _count + 1;
        _audio.GlassB();
        Destroy(_emerald.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    // If all are found enable the gate collider for unlocking it.
    private void EnableGate()
    {
        if (_count == 3)
        {
            _gateCollider.enabled = true;
        }
    }
    // Boss fight walls
    public void BossFight()
    {
        firewall[0].SetActive(true);
        firewall[4].SetActive(true);
    }
    // Firemage, during fight walls
    public void FightWalls()
    {
        for (int i = 1; i < 4; i++)
        {
            firewall[i].SetActive(true);
            _audio.FireFightInc();
        }
    }
    public void StopAllWalls()
    {
        for (int i = 0; i < 5; i++)
        {
            firewall[i].SetActive(false);
        }
        _audio.FireFightStop();
    }
    // Finish the game, start pop animation
    public void GameOver()
    {
        _isGameover = true;
        if (pop != null)
        {
            pop.SetTrigger("Win");
        }
    }
}
