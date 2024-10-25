using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Bounceable : MonoBehaviour
{
    AudioSource audioSource;
    float minPitch = 0.9f, maxPitch = 1.1f;

    Rigidbody rb;
    float movementThreshold = 0.005f;
    [SerializeField] float bounceDamping;
    
    [SerializeField] bool isCaptureDevice;
    GameObject capturedObject;
    bool containsCapture;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Velocity();
    }

    void Velocity()
    {
        if (rb.velocity.magnitude <= movementThreshold)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void AttemptCapture(GameObject obj)
    {
        if (obj.gameObject.GetComponent<Captureable>() && !containsCapture)
        {
            containsCapture = true;
            capturedObject = obj.gameObject;
            obj.gameObject.GetComponent<Captureable>().isCaptured = true;
        }
        else
        {
            print("capture no work");
        }
    }
    public void SummonCapture()
    {
        if (capturedObject != null)
        {
            GameObject summonedCapture = Instantiate(capturedObject, transform.position, Quaternion.identity);
            Destroy(capturedObject);
            summonedCapture.GetComponent<Captureable>().Summon();
            containsCapture = false;
            capturedObject = null;
        }
        else
        {
            print("no capture contained");
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        if (isCaptureDevice && capturedObject == null)
        {
            AttemptCapture(collision.gameObject);
        }
        Vector3 bounceDirection = Vector3.Reflect(-collision.relativeVelocity, collision.GetContact(0).normal);
        rb.velocity = bounceDirection * bounceDamping;
    }
}
