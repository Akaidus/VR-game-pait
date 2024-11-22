using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] List<Checkpoint> checkpoints;
    [SerializeField] GameObject portal;
    bool allCheckpointsCollected;
    int collectedCheckpoints;
    bool hasExecuted;

    void Start()
    {
        portal.SetActive(false);
    }

    void Update()
    {
        if(allCheckpointsCollected) return;
        foreach (var checkpoint in checkpoints)
        {
            bool checkpointCollected = checkpoint.isCollected;
            if (checkpointCollected)
            {
                collectedCheckpoints++;
            }
            else
            {
                collectedCheckpoints = 0;
            }
        }

        if (collectedCheckpoints >= checkpoints.Count)
        {
            AllCollected();
        }
    }

    void AllCollected()
    {
        if(hasExecuted) return;
        allCheckpointsCollected = true;
        portal.SetActive(true);
        hasExecuted = true;
    }
}
