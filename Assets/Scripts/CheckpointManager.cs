using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] List<Checkpoint> checkpoints;
    bool allCheckpointsCollected;
    int collectedCheckpoints;
    bool hasExecuted;
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
        // do something when all checkpoints are taken
        hasExecuted = true;
    }
}
