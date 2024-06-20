using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TriggerZone), typeof(TriggerZone))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TriggerZone _triggerZone;
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

    private void OnEnable()
    {
        _triggerZone.PlayerDetected += Activate;
        _triggerZone.PlayerEscape += Deactivate;
    }

    private void OnDisable()
    {
        _triggerZone.PlayerDetected -= Activate;
        _triggerZone.PlayerEscape -= Deactivate;
    }

    private void Activate()
    {
        StopCoroutine();

        _audioSource.Play();
        _coroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void Deactivate()
    {
        StopCoroutine();

        _coroutine = StartCoroutine(ChangeVolume(_minVolume));
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _step * Time.deltaTime);

            yield return null;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}