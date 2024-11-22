using System;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowableSpawner : MonoBehaviour
{
    [SerializeField] GameObject throwablePrefab;
    Transform playerHand;
    GameObject spawnedThrowable;
    float spawnCooldown;

    void SpawnThrowable()
    {
        if(spawnedThrowable)
            Destroy(spawnedThrowable);
        spawnedThrowable = Instantiate(throwablePrefab, playerHand.position, Quaternion.identity);
    }
    
    void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("PlayerHand")) return;
        if(!other.GetComponent<MemoryForce>().isGrabbing) return;
        playerHand = other.transform;
        SpawnThrowable();
    }
}
