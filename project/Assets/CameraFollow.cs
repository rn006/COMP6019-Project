using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player.
    public float speed = 5f;
    public Vector3 offset; // Distance between camera and player.

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            speed * Time.deltaTime
        );

        transform.position = smoothedPosition;
    }
}
