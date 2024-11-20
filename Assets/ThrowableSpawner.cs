using System;
using UnityEngine;

public class ThrowableSpawner : MonoBehaviour
{
    [SerializeField] GameObject throwablePrefab;
    Transform playerHand;

    GameObject spawnedThrowable;
    // Start is called before the first frame update
    void Start()
    {
            
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnThrowable()
    {
        if(playerHand == null) return;
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
