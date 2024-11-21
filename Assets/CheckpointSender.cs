using UnityEngine;

public class CheckpointSender : MonoBehaviour
{
    [SerializeField] CheckpointReceiver checkpointReceiver;
    [SerializeField] Transform playerPrefab;
    [SerializeField] Transform playerXR;

    void Awake()
    {
        playerXR.position = playerPrefab.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            print("checkpoint obtained");
            checkpointReceiver.latestCheckpoint = other.GetComponent<Checkpoint>().checkpointSpawn;
        }
    }
}
