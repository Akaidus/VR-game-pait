using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RadioPlayer : MonoBehaviour
{
    [SerializeField] private RadioTower radioStation;

    [HideInInspector] public AudioSource audioPlayer;

    [HideInInspector] public bool songTracker = false;

    private bool delayUntilAfterStation = true;

    private void OnEnable()
    {
        if (!delayUntilAfterStation) radioStation.AddListener(this);
    }

    private void Start()
    {
        if (delayUntilAfterStation)
        {
            radioStation.AddListener(this);
            delayUntilAfterStation = false;
        }
    }

    private void OnDisable()
    {
        radioStation.RemoveListener(this);
        songTracker = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!songTracker) return;
        if (!audioPlayer.isPlaying) radioStation.QueueSong();
    }

    public void PlaySong(AudioClip clip)
    {
        audioPlayer.clip = clip;
        audioPlayer.Play();
    }
}