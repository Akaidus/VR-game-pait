using System;
using UnityEngine;

public class ThrowableSpawner : MonoBehaviour
{
    [SerializeField] GameObject throwablePrefab;
    [SerializeField] Transform spawnPosition;
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
        if(spawnPosition == null) return;
        if(spawnedThrowable)
            Destroy(spawnedThrowable);
        spawnedThrowable = Instantiate(throwablePrefab, spawnPosition.position, Quaternion.identity);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            if (spawnPosition == null)
            {
                playerHand = other.transform;
                spawnPosition = playerHand;
            }
            SpawnThrowable();
        }
    }
}
