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
    [SerializeField] private GameObject _hearth;
    [SerializeField] private int _count;
    void Start()
    {
        _audio = GameObject.FindWithTag("Audioman").GetComponent<Audioman>();
        _spawnman = GameObject.FindWithTag("Spawnman").GetComponent<Spawn_man>();
        _gateCollider = GameObject.FindWithTag("Gate").GetComponent<Collider2D>();
        _count = 0;
    }
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
    public void HearthFound()
    {
        _count = _count + 1;
        _spawnman.DifficultyIncreased();
        Destroy(_hearth.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    public void DiamonFound()
    {
        _count = _count + 1;
        _spawnman.DifficultyIncreased();
        Destroy(_diamond.gameObject);
        _audio.CLickAudio();
        EnableGate();
    }
    private void EnableGate()
    {
        if (_count == 2)
        {
            _gateCollider.enabled = true;
        }
    }
    public void GameOver()
    {
        _isGameover = true;
    }

}
