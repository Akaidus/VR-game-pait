using System;
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
        if (other.CompareTag("PlayerHand"))
        {
            playerHand = other.transform;
            SpawnThrowable();
        }
    }
}
