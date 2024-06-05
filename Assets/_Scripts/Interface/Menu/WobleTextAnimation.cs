using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobleTextAnimation : MonoBehaviour
{
    [SerializeField] float wobbleAmount = 0.1f; // Amount of wobble
    [SerializeField] float wobbleSpeed = 1f; // Speed of wobble
    [SerializeField] float wobbleRotationAmount = 5f; // Amount of rotation during wobble
    [SerializeField] float scaleChangeScalar = 10f; // Amount of rotation during wobble

    Vector3 initialPosition;
    Quaternion initialRotation;
    Vector3 initialScale;

    void Start()
    {
        // Store the initial position, rotation, and scale of the TextMeshPro object
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Calculate wobble effect using sine function
        float wobblePositionX = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        float wobblePositionY = Mathf.Sin(Time.time * wobbleSpeed * 1.5f) * wobbleAmount;
        float wobbleRotation = Mathf.Sin(Time.time * wobbleSpeed * 2f) * wobbleRotationAmount;
        float wobbleScale = Mathf.Sin(Time.time * wobbleSpeed) * scaleChangeScalar; // Adjust the factor here

        // Apply wobble to position, rotation, and scale
        transform.localPosition = initialPosition + new Vector3(wobblePositionX, wobblePositionY, 0f);
        transform.localRotation = initialRotation * Quaternion.Euler(0f, 0f, wobbleRotation);
        transform.localScale = initialScale + new Vector3(wobbleScale, wobbleScale, 0f);
    }
}
