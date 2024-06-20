using System;
using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minVolume;
    [SerializeField, Min(0.1f)] private float _maxVolume;
    [SerializeField] private float _step;

    private Coroutine _coroutine;

    private void Start()
    {
        if (_minVolume > _maxVolume)
            _minVolume = _maxVolume;

        _audioSource.volume = _minVolume;
    }

    private void OnDisable()
    {
        _coroutine = null;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<TriggerZone>(out TriggerZone triggerZone))
        {
            StopCoroutine();

            _audioSource.clip = triggerZone.AudioClip;
            _audioSource.Play();
            _coroutine = StartCoroutine(ChangeVolume(_maxVolume));
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<TriggerZone>(out TriggerZone triggerZone))
        {
            StopCoroutine();

            _coroutine = StartCoroutine(ChangeVolume(_minVolume));
        }
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {

        while (Math.Abs(_audioSource.volume - targetVolume) > Mathf.Epsilon)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _step * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}
