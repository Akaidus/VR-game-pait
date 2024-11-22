using System;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowableSpawner : MonoBehaviour
{
    [SerializeField] GameObject throwablePrefab;
    Transform playerHand;
    GameObject spawnedThrowable;

    void SpawnThrowable()
    {
        if(spawnedThrowable)
            Destroy(spawnedThrowable);
        spawnedThrowable = Instantiate(throwablePrefab, playerHand.position, Quaternion.identity);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("PlayerHand")) return;
        if(!other.GetComponent<MemoryForce>().handOccupied) return;
        playerHand = other.transform;
        SpawnThrowable();
    }
}
