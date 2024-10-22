using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PingPongBall : MonoBehaviour
{
    AudioSource audioSource;
    float minPitch = 0.9f, maxPitch = 1.1f;

    Rigidbody rb;
    float movementThreshold = 0.005f;
    [SerializeField] float bounceDamping;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude <= movementThreshold)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 bounceDirection = Vector3.Reflect(-collision.relativeVelocity, collision.GetContact(0).normal);
        rb.velocity = bounceDirection * bounceDamping;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
