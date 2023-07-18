using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _soundIncreaseDuraion;
    [SerializeField] private float _soundDecreaseDuraion;

    private AudioSource _audioSource;
    private Animator _animator;
    private Coroutine _changeVolumeJob;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_changeVolumeJob is not null)
            StopCoroutine(_changeVolumeJob);

        _audioSource.Play();
        _changeVolumeJob = StartCoroutine(ChangeVolume(1, _soundIncreaseDuraion));
        _animator.SetBool("IsSet", true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_changeVolumeJob is not null)
            StopCoroutine(_changeVolumeJob);

        _changeVolumeJob = StartCoroutine(ChangeVolume(0, _soundDecreaseDuraion));
        _animator.SetBool("IsSet", false);
    }

    private IEnumerator ChangeVolume(float targetVolume, float duration)
    {
        float volumeStep = 1f / duration;

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, volumeStep * Time.deltaTime);
            yield return null;
        }

        if(_audioSource.volume == 0)
            _audioSource.Stop();
    }
}
