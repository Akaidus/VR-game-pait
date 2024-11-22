using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public Transform checkpointSpawn;
    public bool isCollected;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("checkpoint collected");
            isCollected = true;
        }
    }
}
