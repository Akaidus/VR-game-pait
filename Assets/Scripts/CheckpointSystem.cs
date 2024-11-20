using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] Vector3 spawnOffset;
    Transform latestCheckpoint;
    
    // Set this void to an action event from the player's controller.
    public void RespawnAtLatestCheckpoint()
    {
        // Will not run the function if no checkpoint.
        if(latestCheckpoint == null) return;
        // Sets the players position to that of the checkpoint with an offset.
        transform.position = latestCheckpoint.position + spawnOffset;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            latestCheckpoint = other.transform;
        }
    }
}
