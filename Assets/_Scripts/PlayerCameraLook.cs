using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLook : MonoBehaviour
{
    [SerializeField] private Transform playersBody;

    [SerializeField] InputManager inputManager;
    [Space]
    [SerializeField] float mouseSense = 0.05f;
    [SerializeField] float rotoationSmoothTime = 0.025f;
    [SerializeField] bool blockHorizontalLook = false;
    [SerializeField] bool blockVerticalLook = false;
    [Range(-90, 90f)][SerializeField] float verticalLockAngle = 0f;
    [Range(-180f, 180f)][SerializeField] float horizontalLockAngle = 0f;
    [Space]

    Vector2 mouseDelta;
    float xRotation = 0f;
    float xRotationVelocity = 0f;
    float yRotationVelocity = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        mouseDelta = inputManager.inputMaster.CameraLook.Look.ReadValue<Vector2>() * mouseSense * Time.deltaTime;
    }

    private void LateUpdate()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        xRotation = Mathf.SmoothDamp(xRotation, xRotation - mouseDelta.y, ref xRotationVelocity, rotoationSmoothTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(blockVerticalLook ? horizontalLockAngle : xRotation, 0f, 0f);

        float yRotation = Mathf.SmoothDamp(0f, mouseDelta.x, ref yRotationVelocity, rotoationSmoothTime);
        if (!blockHorizontalLook)
        {
            playersBody.Rotate(0f, yRotation, 0f);
        }
        else
        {
            playersBody.localRotation = Quaternion.Euler(playersBody.localRotation.x, verticalLockAngle, playersBody.localRotation.z);
        }
    }
}
