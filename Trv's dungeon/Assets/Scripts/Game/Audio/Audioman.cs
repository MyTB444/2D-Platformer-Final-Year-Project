using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Audioman : MonoBehaviour
{
    //Simple audio manager playing the assigned audio
    public AudioClip win;
    public AudioClip regen;
    public AudioClip upgrade;
    public AudioClip glass;
    public AudioClip gatemov;

    //--------------
    public AudioClip fireFight;
    public AudioClip normalBG;
    public AudioClip horns;
    [SerializeField] private AudioClip _clickClip;
    private AudioSource _audioSource;
    public AudioSource fireFighBG;
    public AudioSource bg;
    public AudioSource events;
    public Transform player;
    //private int x = 0;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        BGMusic();
    }

    public void CLickAudio()
    {
        _audioSource.clip = _clickClip;
        _audioSource.Play();
    }
    public void FireFight()
    {
        fireFighBG.clip = fireFight;
        fireFighBG.Play();
    }
    public void FireFightInc()
    {
        fireFighBG.volume = 0.25f;
    }
    public void FireFightStop()
    {
        fireFighBG.Stop();
    }
    private void BGMusic()
    {
        if (bg.volume == 0.04f && player.transform.position.y < -4)
        {
            bg.volume = 0.02f;
        }
        if (bg.volume != 0.04f && player.transform.position.y >= -4)
        {
            bg.volume = 0.04f;
        }
        if (player.transform.position.y < -18)
        {
            bg.volume = 0f;
        }
    }
    public void Horns()
    {
        events.clip = horns;
        events.Play();
    }
    public void Upgrade()
    {
        events.clip = upgrade;
        events.Play();
    }
    public void RegenA()
    {
        events.clip = regen;
        events.Play();
    }
    public void GlassB()
    {
        events.clip = glass;
        events.Play();
    }
    public void GateMove()
    {
        events.clip = gatemov;
        events.Play();
    }
    public void WinS()
    {
        events.clip = win;
        events.Play();
    }
}
