using System;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;

    [SerializeField] bool isMap;
    [SerializeField] bool followPosition;
    [SerializeField] Vector3 positionOffset;
    [SerializeField] bool followRotation;
    [SerializeField] bool followOnlyYRotation;
    [SerializeField] Vector3 rotationOffset;

    void LateUpdate()
    {
        if(followPosition && !isMap)
            transform.position = targetToFollow.position + positionOffset;
        else if (isMap)
            // Set position as if it was a child of the target GameObject.
            transform.position = targetToFollow.TransformPoint(positionOffset);
        if(followRotation || !followOnlyYRotation)
            transform.rotation = targetToFollow.rotation * Quaternion.Euler(rotationOffset);
        if (followOnlyYRotation)
            // Make the transform only rotate on the y axis.
            transform.forward = Vector3.ProjectOnPlane(targetToFollow.forward,Vector3.up).normalized;
    }
}
