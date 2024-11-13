using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StretchArm : MonoBehaviour
{
    [SerializeField] Transform target; 
    [SerializeField] Transform root;
    [SerializeField] Transform mid;    
    [SerializeField] Transform tip;
    [SerializeField] Animator animator;
    [SerializeField] float stretchSpeed = 10f;
    TwoBoneIKConstraint twoBoneIK;

    void Start()
    {
        twoBoneIK = GetComponent<TwoBoneIKConstraint>();
    }

    void Update()
    {
        animator.enabled = false;
        
        // Calculate the direction from root to target
        Vector3 directionToHand = target.position - root.position;

        // Calculate the current arm length from root to end
        float currentArmLength = Vector3.Distance(root.position, tip.position);

        // Calculate the new desired arm length from root to target
        float desiredArmLength = directionToHand.magnitude;

        // Calculate the stretch factor
        float stretchFactor = desiredArmLength / currentArmLength;

        Vector3 directionToElbow = mid.position - root.position;

        // Stretch the mid bone based on the stretch factor (along the arm axis)
        Vector3 newElbowPosition = root.position + directionToElbow.normalized * (currentArmLength * stretchFactor);

        // Apply the new position of the mid bone
        mid.position = Vector3.Lerp(mid.position, newElbowPosition, Time.deltaTime * stretchSpeed);

        animator.enabled = true;

        //twoBoneIK.data.mid.rotation = mid.rotation;
        //UpdateOtherBones();

        print("stretch factor: " + stretchFactor);
        print("desired arm length: " + desiredArmLength);
        print("current arm length: " + currentArmLength);
    }

    void UpdateOtherBones()
    {
        root.position = twoBoneIK.data.root.position;
        root.rotation = twoBoneIK.data.root.rotation;

        mid.rotation = twoBoneIK.data.mid.rotation;
        
        tip.position = twoBoneIK.data.tip.position;
        tip.rotation = twoBoneIK.data.tip.rotation;
    }
}
