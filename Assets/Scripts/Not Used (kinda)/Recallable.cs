using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recallable : MonoBehaviour
{
    public bool isRecallable;
    public bool isHeld;
    public List<Collider> colliders;

    void Start()
    {
        colliders.Clear();
        if (GetComponent<Collider>())
        {
            colliders.Add(GetComponent<Collider>());
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Collider>()) continue;
            var childCollider = transform.GetChild(i).GetComponent<Collider>();
            colliders.Add(childCollider);
        }
        
    }
}
