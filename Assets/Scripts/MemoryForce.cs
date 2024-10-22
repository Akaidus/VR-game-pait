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

    Collider[] colliders;
    float fistValue;
    
    public void RecallObject()
    {
        if(objectToRecall is null) return;
        recallableObject = objectToRecall.GetComponent<Recallable>();
        if(handOccupied) return;
        if(!recallableObject.isRecallable) return;
        if(recallableObject.isHeld) return;
        print("TEST");
        isRecalling = true;
        foreach (var col in recallableObject.colliders)
        {
            col.isTrigger = true;
        }
        objectToRecall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        var speed = recallSpeed * Time.deltaTime;
        var objectToRecallPos = objectToRecall.transform.position;
        objectToRecallPos = Vector3.MoveTowards(objectToRecallPos, transform.position, speed);
        objectToRecall.transform.position = objectToRecallPos;
        
    }
    
    void Update()
    {
        float actionValue = RecallAction.action.ReadValue<float>();
        if (actionValue == 1)
        {
            RecallObject();
        }
        else if (actionValue < 1)
        {
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
            recallableObject = objectToRecall.GetComponent<Recallable>();
            foreach (var col in recallableObject.colliders)
            {
                col.isTrigger = false;
            }
            handOccupied = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToRecall)
        {
            recallableObject = objectToRecall.GetComponent<Recallable>();
            foreach (var col in recallableObject.colliders)
            {
                col.isTrigger = false;
            }
            handOccupied = false;
        }
    }
    
    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        handOccupied = true;
        objectToRecall = args.interactableObject.transform.gameObject;
        objectToRecall.GetComponent<Recallable>().isHeld = true;
        print($"i touch {args.interactableObject}");
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        handOccupied = false;
        objectToRecall.GetComponent<Recallable>().isHeld = false;
        print($"i throw {args.interactableObject}");
    }
}
