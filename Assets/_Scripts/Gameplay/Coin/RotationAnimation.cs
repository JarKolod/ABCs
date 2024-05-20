using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
