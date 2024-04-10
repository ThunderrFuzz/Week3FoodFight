using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // The object the camera will follow
    public Vector3 offset; // The offset from the target position
    public float smoothSpeed = 0.125f; // The speed of camera movement

    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera in the scene
    }

    void LateUpdate()
    {
        if (target != null && mainCamera != null)
        {
            Vector3 desiredPosition = target.position + offset; // Target's position with offset
            Vector3 smoothedPosition = Vector3.Lerp(mainCamera.transform.position, desiredPosition, smoothSpeed); // Smoothly interpolate between current camera position and target position
            mainCamera.transform.position = smoothedPosition; // Update camera position
        }
    }
}
