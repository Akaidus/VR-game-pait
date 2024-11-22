using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isCollected;
    [SerializeField] MeshRenderer flag;
    [SerializeField] MeshRenderer button;
    [SerializeField] Material newColor;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(isCollected) return;
        if (other.CompareTag("Player") || other.CompareTag("PlayerHand"))
        {
            audioSource.Play();
            print("checkpoint collected");
            Material[] newFlag = flag.materials;
            Material[] newButton = button.materials;

            newFlag[newFlag.Length - 1] = newColor;
            newButton[newButton.Length - 1] = newColor;

            flag.materials = newFlag;
            button.materials = newButton;
            
            isCollected = true;
        }
    }
}
