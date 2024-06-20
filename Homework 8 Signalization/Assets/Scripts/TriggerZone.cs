using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    public AudioClip AudioClip => _audioClip;
}