using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class MemoryForce : MonoBehaviour
{
    [SerializeField] InputActionProperty RecallAction;

    [HideInInspector] public bool isGrabbing;
    
    // isGrabbing er det eneste, der bliver brugt i denne her kode, da den resterende kode er fra Nikolajs andet projekt
    // Den tjekker, om man trykker på Grab-knappen på VR-controlleren
    void Update()
    {
        float actionValue = RecallAction.action.ReadValue<float>();
        
        if (actionValue == 1)
        {
            isGrabbing = true;
        }
        else if (actionValue < 1)
        {
            isGrabbing = false;
        }
    }
}
