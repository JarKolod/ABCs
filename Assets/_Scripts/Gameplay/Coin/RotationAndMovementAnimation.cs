using UnityEngine;

public class RotationAndMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30f;
    [SerializeField] float moveDistance = 0.5f;
    [SerializeField] float smoothTime = 0.5f;
    [SerializeField] float smoothness = 0.1f;
    private float currentVelocity = 0f;

    void LateUpdate()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        float targetY = Mathf.Sin(Time.time) * moveDistance + transform.position.y;
        transform.position = new Vector3(transform.position.x, Mathf.SmoothDamp(transform.position.y, targetY, ref currentVelocity, smoothTime, smoothness), transform.position.z);
    }
}
