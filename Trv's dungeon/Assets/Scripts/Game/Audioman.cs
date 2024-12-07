using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioman : MonoBehaviour
{
    [SerializeField] private AudioClip _clickClip;
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void CLickAudio()
    {
        _audioSource.clip = _clickClip;
        _audioSource.Play();
    }
}
