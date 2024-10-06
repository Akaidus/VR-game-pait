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
    [SerializeField] float recallSpeed;

    float fistValue;
    
    public void RecallObject()
    {
        if(objectToRecall is null) return;
        if(handOccupied) return;
        if(!objectToRecall.GetComponent<Recallable>().isRecallable) return;
        if(objectToRecall.GetComponent<Recallable>().isHeld) return;
        print("TEST");
        isRecalling = true;
        objectToRecall.GetComponent<Collider>().isTrigger = true;
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
            objectToRecall.GetComponent<Collider>().isTrigger = false;
            isRecalling = false;
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToRecall)
        {
            objectToRecall.GetComponent<Collider>().isTrigger = false;
            handOccupied = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToRecall)
        {
            objectToRecall.GetComponent<Collider>().isTrigger = false;
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
