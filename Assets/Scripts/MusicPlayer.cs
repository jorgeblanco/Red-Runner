using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgm;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (bgm.Length > 0)
        {
            _audioSource.clip = bgm[Random.Range(0, bgm.Length)];
            _audioSource.Play();
        }
    }
}
