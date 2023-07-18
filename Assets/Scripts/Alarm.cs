using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _soundIncreaseDuration;
    [SerializeField] private float _soundDecreaseDuration;

    private AudioSource _audioSource;
    private Animator _animator;
    private Coroutine _changeVolumeJob;
    private int _isSetParameterHash = Animator.StringToHash("IsSet");
    private float _playSoundThreshold = 0;
    private bool _isTurnedOn = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void SwitchState()
    {
        if (_changeVolumeJob is not null)
            StopCoroutine(_changeVolumeJob);

        _changeVolumeJob = StartCoroutine(
            _isTurnedOn
            ? ChangeVolume(0, _soundDecreaseDuration)
            : ChangeVolume(1, _soundIncreaseDuration)
            );

        _isTurnedOn = !_isTurnedOn;
        _animator.SetBool(_isSetParameterHash, _isTurnedOn);
    }

    private IEnumerator ChangeVolume(float targetVolume, float duration)
    {
        float volumeStep = 1f / duration;

        if (targetVolume > _audioSource.volume)
            _audioSource.Play();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, volumeStep * Time.deltaTime);
            yield return null;
        }

        if(_audioSource.volume <= _playSoundThreshold)
            _audioSource.Stop();
    }
}
