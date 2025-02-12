using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_audio : MonoBehaviour
{
    // Hold clips and assign/play those clips when called.
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private AudioClip _damageClip;
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private AudioClip _dashClip;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void SwingAudio()
    {
        _audioSource.clip = _attackClip;
        _audioSource.Play();
    }
    public void JumpAudio()
    {
        _audioSource.clip = _jumpClip;
        _audioSource.Play();
    }
    public void DamageAudio()
    {
        _audioSource.clip = _damageClip;
        _audioSource.Play();
    }
    public void DeathAudio()
    {
        _audioSource.clip = _deathClip;
        _audioSource.Play();
    }
    public void DashAudio()
    {
        _audioSource.clip = _dashClip;
        _audioSource.Play();
    }
}


