using UnityEngine;
using UnityEngine.SpatialTracking;

public class PoseDriveController : MonoBehaviour
{
    [SerializeField] TrackedPoseDriver mainCamera;
    
    // Bliver brugt til at skifte fra TrackRotationOnly() til TrackRotationAndPosition() gennem events i animationen på "Steal Legs".
    public void TrackRotationOnly()
    {
        mainCamera.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
    }

    public void TrackRotationAndPosition()
    {
        mainCamera.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
    }
}
