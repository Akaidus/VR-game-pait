using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 spawnOffset;
    Transform latestCheckpoint;
    
    // Set this void to an action event from the player's controller.
    public void RespawnAtLatestCheckpoint()
    {
        // Will not run the function if no checkpoint.
        if (latestCheckpoint != null)
        {
            // Sets the players position to that of the checkpoint.
            player.position = latestCheckpoint.position;
            print("player respawn");
        }
        else
        {
            print("tp not work");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            print("checkpoint obtained");
            latestCheckpoint = other.GetComponent<Checkpoint>().checkpointSpawn;
        }
    }
}
