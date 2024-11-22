using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField, Range(0f, 1f)] private float volumeMin = 0.8f;
    [SerializeField, Range(0f, 1f)] private float volumeMax = 1f;
    [SerializeField, Range(0f, 3f)] private float pitchMin = 0.85f;
    [SerializeField, Range(0f, 3f)] private float pitchMax = 1.2f;

    void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.volume = Random.Range(volumeMin, volumeMax);
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.Play();
    }
}
