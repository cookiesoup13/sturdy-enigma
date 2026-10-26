using UnityEngine;
 
public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5.0f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public bool shouldFollow = true;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
