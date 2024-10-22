using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInside : MonoBehaviour
{
    [SerializeField] GameObject cup;
    [SerializeField] GameObject confetti;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PingPongBall"))
        {
            audioSource.PlayOneShot(audioClips[0]);
            audioSource.PlayOneShot(audioClips[1]);
            if(isHit) return;
            confetti.SetActive(true);
            Destroy(other.gameObject);
            Destroy(cup, 2f);
            isHit = true;
        }
    }
}
