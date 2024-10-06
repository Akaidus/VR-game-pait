using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] InputActionProperty pinchAnimation;
    [SerializeField] InputActionProperty fistAnimation;

    Animator handAnimator;
    void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float pinchValue = pinchAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Pinch", pinchValue);
        float fistValue = fistAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Fist", fistValue);
    }
}
