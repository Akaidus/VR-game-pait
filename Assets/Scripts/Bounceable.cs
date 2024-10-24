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
    bool hasCaptured;
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

    public void SummonCapture()
    {
        if (capturedObject != null)
        {
            GameObject summonedCapture = Instantiate(capturedObject);
            var summonSpeed = 1.5f;
            summonedCapture.GetComponent<Rigidbody>().velocity = Vector3.forward * summonSpeed;
            capturedObject = null;
        }
    }

    IEnumerator ShrinkCapture(float waitTime, GameObject obj)
    {
        Vector3 originalSize = new Vector3(1, 1, 1);
        Vector3 newSize = new Vector3(0, 0, 0);
        var t = waitTime;
        //var time = waitTime;
        while (t > 0)
        {
            t -= Time.deltaTime;
            print("shrink" + t);
            obj.transform.localScale = new Vector3(t, t, t);
            if (t < 0)
                t = 0;
                //obj.transform.localScale = Vector3.Lerp(originalSize,newSize,t);
        }
        yield return new WaitForSeconds(waitTime-0.1f);
        //var shrinkVal = 0.1f;
        //obj.transform.localScale -= new Vector3(shrinkVal * Time.deltaTime,shrinkVal * Time.deltaTime,shrinkVal * Time.deltaTime);
        //obj.transform.localScale = Vector3.Lerp(originalSize,newSize,waitTime*Time.time);
        //yield return new WaitForSeconds(waitTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        if (isCaptureDevice)
        {
            if (capturedObject == null)
            {
                if (collision.gameObject.GetComponent<Captureable>() && !hasCaptured)
                {
                    capturedObject = collision.gameObject;
                    var delay = 1;
                    StartCoroutine(ShrinkCapture(delay, collision.gameObject));
                    Destroy(collision.gameObject, delay);
                    hasCaptured = true;
                }
            }
            Vector3 bounceDirection = Vector3.Reflect(-collision.relativeVelocity, collision.GetContact(0).normal);
            rb.velocity = bounceDirection * bounceDamping;
        }
        else
        {
            Vector3 bounceDirection = Vector3.Reflect(-collision.relativeVelocity, collision.GetContact(0).normal);
            rb.velocity = bounceDirection * bounceDamping;
        }
    }
}
