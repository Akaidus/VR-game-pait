using UnityEngine;
using UnityEngine.SpatialTracking;

public class PoseDriveController : MonoBehaviour
{
    [SerializeField] TrackedPoseDriver mainCamera;
    
    public void TrackRotationOnly()
    {
        mainCamera.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
    }

    public void TrackRotationAndPosition()
    {
        mainCamera.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
    }
}
