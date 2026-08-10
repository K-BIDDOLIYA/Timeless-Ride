using UnityEngine;

// Simple smoothed follow camera. Attach to Main Camera, assign the car as target.
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(3f, 2f, -10f);
    public float smoothTime = 0.25f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}
