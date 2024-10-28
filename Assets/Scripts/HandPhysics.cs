using System;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    public bool handTouching;
    [SerializeField] HandPhysics otherHand;

    ConfigurableJoint joint;
    float initialYDrive;
    [SerializeField] float boostedYDrive;
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        initialYDrive = joint.yDrive.positionSpring;
    }

    void Update()
    {
        if (handTouching && otherHand.handTouching)
        {
            //joint.yDrive.positionSpring = boostedYDrive;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        handTouching = true;
    }

    void OnCollisionExit(Collision other)
    {
        handTouching = false;
    }
}
