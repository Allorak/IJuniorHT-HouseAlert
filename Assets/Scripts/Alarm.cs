using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _soundIncreaseDuraion;
    [SerializeField] private float _soundDecreaseDuraion;

    private AudioSource _audioSource;
    private Animator _animator;
    private Coroutine _changeVolumeJob;
    private int _isSetParameterHash = Animator.StringToHash("IsSet");

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void TurnOn()
    {
        if (_changeVolumeJob is not null)
            StopCoroutine(_changeVolumeJob);

        _audioSource.Play();
        _changeVolumeJob = StartCoroutine(ChangeVolume(1, _soundIncreaseDuraion));
        _animator.SetBool(_isSetParameterHash, true);
    }

    public void TurnOff()
    {
        if (_changeVolumeJob is not null)
            StopCoroutine(_changeVolumeJob);

        _changeVolumeJob = StartCoroutine(ChangeVolume(0, _soundDecreaseDuraion));
        _animator.SetBool(_isSetParameterHash, false);
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
