using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_man : MonoBehaviour
{
    private bool _isGameover = false;
    private Spawn_man _spawnman;
    private Collider2D _gateCollider;
    private Audioman _audio;
    [SerializeField] private GameObject _gate;
    [SerializeField] private GameObject _diamond;
    [SerializeField] private GameObject _emerald;
    [SerializeField] private GameObject _gem;
    [SerializeField] private int _count;
    void Start()
    {
        _audio = GameObject.FindWithTag("Audioman").GetComponent<Audioman>();
        _spawnman = GameObject.FindWithTag("Spawnman").GetComponent<Spawn_man>();
        if (GameObject.FindWithTag("Gate") != null)
        {
            _gateCollider = GameObject.FindWithTag("Gate").GetComponent<Collider2D>();
        }
        _count = 0;
    }
    //Restart and exit buttons.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameover == true || Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown(KeyCode.H) && _isGameover == true)
        {
            Application.Quit();
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
    // If hearth or diamond is found, inform related objects.
    public void GemFound()
    {
        _count = _count + 1;
        Destroy(_gem.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    public void DiamondFound()
    {
        _count = _count + 1;
        Destroy(_diamond.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    public void EmeraldFound()
    {
        _count = _count + 1;
        Destroy(_emerald.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    // If both are found enable the gate collider for unlocking it.
    private void EnableGate()
    {
        if (_count == 3)
        {
            _gateCollider.enabled = true;
        }
    }
    public void GameOver()
    {
        _isGameover = true;
    }

}
