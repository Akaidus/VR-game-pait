using System;
using UnityEngine;

public class Captureable : MonoBehaviour
{
    //[SerializeField] public GameObject prefab;
    Vector3 originalSize = new(1, 1, 1);
    Vector3 newSize = new(0, 0, 0);
    float duration;
    float multiplier = 1.8f;
    [HideInInspector] public bool isCaptured;
    bool isSummoned;
    new Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isCaptured)
        {
            print("capt");
            Shrink();
        }
        if (isSummoned)
        {
            print("summ");
            Expand();
        }
    }

    void Shrink()
    {
        duration += Time.deltaTime * multiplier;
        print("shrink" + duration);
        transform.localScale = Vector3.Lerp(originalSize,newSize, duration);
        if(transform.localScale == newSize)
        {
            collider.enabled = false;
            isCaptured = false;
        }
    }

    void Expand()
    {
        isSummoned = true;
        transform.localScale = Vector3.Lerp(originalSize,newSize, duration);
        if (transform.localScale == originalSize)
        {
            print("full expanded");
            collider.enabled = true;
            isSummoned = false;
        }
    }

    public void Summon()
    {
        transform.localScale = newSize;
        collider.enabled = true;
        var delay = 0.1f;
        Invoke(nameof(Expand), delay);
    }
}
