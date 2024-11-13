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

    bool handOccupied;
    bool isRecalling;
    GameObject objectToRecall;
    Recallable recallableObject;
    [SerializeField] float recallSpeed;
    float initialRecallSpeed;
    XRDirectInteractor xrDirectInteractor;
    Collider[] objectColliders;
    float fistValue;

    void Start()
    {
        initialRecallSpeed = recallSpeed;
        xrDirectInteractor = GetComponent<XRDirectInteractor>();
    }

    public void RecallObject()
    {
        if(objectToRecall is null) return;
        //if (!objectToRecall.GetComponent<Recallable>()) return;
        recallableObject = objectToRecall.GetComponent<Recallable>();
        if(handOccupied) return;
        if(!recallableObject.isRecallable) return;
        if(recallableObject.isHeld) return;
        print(recallSpeed);
        isRecalling = true;
        foreach (var col in recallableObject.colliders)
        {
            col.isTrigger = true;
        }
        objectToRecall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        recallSpeed += 0.6f;
        var speed = recallSpeed * Time.deltaTime;
        var objectToRecallPos = objectToRecall.transform.position;
        objectToRecallPos = Vector3.MoveTowards(objectToRecallPos, xrDirectInteractor.attachTransform.transform.position, speed);
        objectToRecall.transform.position = objectToRecallPos;
    }
    
    void Update()
    {
        float actionValue = RecallAction.action.ReadValue<float>();
        if (actionValue == 1)
        {
            //collider.enabled = true;
            RecallObject();
        }
        else if (actionValue < 1)
        {
            //collider.enabled = false;
            if(!objectToRecall) return;
            if(!isRecalling) return;
            recallableObject = objectToRecall.GetComponent<Recallable>();
            foreach (var col in recallableObject.colliders)
            {
                col.isTrigger = false;
            }
            isRecalling = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToRecall)
        {
            if (objectToRecall.GetComponent<Recallable>())
            {
                recallSpeed = initialRecallSpeed;
                recallableObject = objectToRecall.GetComponent<Recallable>();
                foreach (var col in recallableObject.colliders)
                {
                    col.isTrigger = false;
                }
                handOccupied = true;
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToRecall)
        {
            if (objectToRecall.GetComponent<Recallable>())
            {
                recallSpeed = initialRecallSpeed;
                recallableObject = objectToRecall.GetComponent<Recallable>();
                foreach (var col in recallableObject.colliders)
                {
                    col.isTrigger = false;
                }
                handOccupied = false;
            }
        }
    }
    
    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        handOccupied = true;
        if (args.interactableObject.transform.gameObject.GetComponent<Recallable>())
        {
            objectToRecall = args.interactableObject.transform.gameObject;
            objectToRecall.GetComponent<Recallable>().isHeld = true;
        }
        print($"i touch {args.interactableObject}");
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        handOccupied = false;
        if (args.interactableObject.transform.gameObject.GetComponent<Recallable>())
            objectToRecall.GetComponent<Recallable>().isHeld = false;
        print($"i throw {args.interactableObject}");
    }
}
