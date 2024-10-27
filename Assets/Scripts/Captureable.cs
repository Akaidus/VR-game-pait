using System;
using Unity.VisualScripting;
using UnityEngine;

public class Captureable : MonoBehaviour
{
    public GameObject prefab;
    Vector3 originalSize = new(1, 1, 1), newSize = new(0, 0, 0);
    float sizeDuration, moveDuration;
    float sizeMultiplier = 4f, moveMultiplier = 0.9f;
    [HideInInspector] public bool isBeingCaptured;
    bool isBeingSummoned;
    new Collider collider;
    GameObject myBall;

    void Start()
    {
        collider = GetComponent<Collider>();
        prefab = transform.gameObject;
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

    void Shrink()
    {
        collider.enabled = false;
        sizeDuration += Time.deltaTime * sizeMultiplier;
        moveDuration += Time.deltaTime * moveMultiplier;
        transform.localScale = Vector3.Lerp(originalSize,newSize, sizeDuration);
        transform.position = Vector3.MoveTowards(transform.position,myBall.transform.position, moveDuration);
        if (transform.localScale != newSize) return;
        print("fully shrunk");
        transform.parent = myBall.transform;
        isBeingCaptured = false;
    }

    void Expand()
    {
        isBeingSummoned = true;
        sizeDuration += Time.deltaTime * sizeMultiplier;
        moveDuration += Time.deltaTime * moveMultiplier;
        transform.localScale = Vector3.Lerp(newSize, originalSize, sizeDuration);
        if (transform.localScale != originalSize) return;
        print("fully expanded");
        sizeDuration = 0;        
        moveDuration = 0;
        collider.enabled = true;
        myBall = null;
        isBeingSummoned = false;
    }

    public void Summon()
    {
        sizeDuration = 0;        
        moveDuration = 0;
        transform.localScale = newSize;
        transform.Translate(0,2,1);
        var delay = 0.1f;
        Invoke(nameof(Expand), delay);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bounceable>() == null) return;
        if (collision.gameObject.GetComponent<Bounceable>().isCaptureDevice)
        {
            myBall = collision.gameObject;
        }
    }
}
