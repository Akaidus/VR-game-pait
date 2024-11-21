using Unity.XR.CoreUtils;
using UnityEngine;

public class CheckpointReceiver : MonoBehaviour
{
    [SerializeField] Transform playerPrefab;
    [SerializeField] GameObject playerXR;
    [HideInInspector] public Transform latestCheckpoint;
    
    // Set this void to an action event from the player's controller.
    public void RespawnAtLatestCheckpoint()
    {
        // Will not run the function if no checkpoint.
        if (latestCheckpoint != null)
        {
            // The Player XR and PlayerPrefab may not always have the same position.
            // The PlayerPrefab's pos is therefor set to that of the checkpoint and Player XR's GO is turned on and off since the physics does not allow it to be moved properly.
            DisableXR();
            playerPrefab.position = latestCheckpoint.position;
            var delay = 0.1f;
            Invoke(nameof(EnableXR), delay);
            print("player respawn");
        }
        else
        {
            print("tp not work");
        }
    }

    void DisableXR()
    {
        //playerXR.GetComponent<XROrigin>().enabled = false;
        playerXR.SetActive(false);
    }

    void EnableXR()
    {
        //playerXR.GetComponent<XROrigin>().enabled = true;
        playerXR.SetActive(true);
    }
}
