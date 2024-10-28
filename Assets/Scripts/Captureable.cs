using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Captureable : MonoBehaviour
{
    Rigidbody rb;
    Vector3 originalSize, shrunkSize = new(0, 0, 0);
    float sizeDuration, moveDuration;
    float sizeMultiplier = 4f, moveMultiplier = 0.9f;
    [HideInInspector] public bool isBeingCaptured;
    bool isBeingSummoned;
    new Collider collider;
    GameObject myBall;

    void Start()
    {
        collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        originalSize = transform.localScale;
    }

    void Update()
    {
        if (isBeingCaptured)
        {
            print("capturing");
            Shrink();
        }
        if (isBeingSummoned)
        {
            print("summoning capture");
            Expand();
        }
    }

    public void StartShrink()
    {
        collider.enabled = false;
        isBeingCaptured = true;
    }
    
    void Shrink()
    {
        sizeDuration += Time.deltaTime * sizeMultiplier;
        moveDuration += Time.deltaTime * moveMultiplier;
        transform.localScale = Vector3.Lerp(originalSize,shrunkSize, sizeDuration);
        transform.position = Vector3.MoveTowards(transform.position,myBall.transform.position, moveDuration);
        if (transform.localScale != shrunkSize) return;
        print("fully shrunk");
        rb.isKinematic = true;
        transform.parent = myBall.transform;
        isBeingCaptured = false;
    }

    void Expand()
    {
        sizeDuration += Time.deltaTime * sizeMultiplier;
        moveDuration += Time.deltaTime * moveMultiplier;
        rb.AddForce(0, 8, 5);
        transform.localScale = Vector3.Lerp(shrunkSize, originalSize, sizeDuration);
        if (transform.localScale != originalSize) return;
        print("fully expanded");
        collider.enabled = true;
        sizeDuration = 0;        
        moveDuration = 0;
        myBall = null;
        isBeingSummoned = false;
    }

    public void Summon()
    {
        sizeDuration = 0;        
        moveDuration = 0;
        transform.localScale = shrunkSize;
        rb.freezeRotation = true;
        rb.isKinematic = false;
        isBeingSummoned = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        rb.freezeRotation = false;
        if (collision.gameObject.GetComponent<Bounceable>() == null) return;
        if (collision.gameObject.GetComponent<Bounceable>().isCaptureDevice)
        {
            myBall = collision.gameObject;
        }
    }
}
