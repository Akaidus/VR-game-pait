using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyPhysics : MonoBehaviour
{
    [SerializeField] Transform playerHead;
    [SerializeField] Transform leftController;
    [SerializeField] Transform rightController;

    [SerializeField] ConfigurableJoint leftHandJoint;
    [SerializeField] ConfigurableJoint rightHandJoint;
    
    
    [SerializeField] CapsuleCollider playerCollider;

    [SerializeField] float playerHeightMin; //default 0.4f
    [SerializeField] float playerHeightMax; //default 2.2f
    
    void FixedUpdate()
    {
        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;
    }
}
