using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script by Valem on YouTube

[System.Serializable]
public class VRMap
{
    [SerializeField] Transform vrTarget;
    [SerializeField] Transform rigTarget;
    [SerializeField] Vector3 positionOffset;
    [SerializeField] Vector3 rotationOffset;

    public void Map()
    {
        //Return world position as if it was a child of the GameObject
        rigTarget.position = vrTarget.TransformPoint(positionOffset);
        //Set rotation with offset
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(rotationOffset);
    }
}
public class VRRigFollow : MonoBehaviour
{
    [SerializeField] VRMap head;
    [SerializeField] VRMap leftHand;
    [SerializeField] VRMap rightHand;
    
    [SerializeField] Transform headTarget;
    [SerializeField] Vector3 positionOffset;
    void Start()
    {
        positionOffset = transform.position - positionOffset;
    }

    void Update()
    {
        // Make body follow head.
        transform.position = headTarget.position + positionOffset;
        // Make the body only rotate on the y axis,
        transform.forward = Vector3.ProjectOnPlane(headTarget.up, Vector3.up).normalized;
        
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
