using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLook : MonoBehaviour
{
    [SerializeField] private Transform playersBody;

    [SerializeField] InputManager inputManager;
    [Space]
    [SerializeField] float mouseSense = 0.05f;
    [SerializeField] float rotationSmoothTime = 0.025f;
    [SerializeField] bool blockHorizontalLook = false;
    [SerializeField] bool blockVerticalLook = false;
    [Range(-180, 180f)][SerializeField] float verticalLockAngle = 0f;
    [Range(-180f, 180f)][SerializeField] float horizontalLockAngle = 0f;
    [Space]

    private float lastTimeScale = 1f;
    private Vector2 mouseDelta;
    private float xRotation = 0f;
    private float xRotationVelocity = 0f;
    private float yRotationVelocity = 0f;

    private void Update()
    {
        mouseDelta = inputManager.inputMaster.CameraLook.Look.ReadValue<Vector2>() * mouseSense * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (Time.timeScale != 0)
        {
            RotatePlayer();
        }

        if (Time.timeScale != lastTimeScale)
        {
            OnTimeScaleChanged();
            lastTimeScale = Time.timeScale;
        }
    }

    private void RotatePlayer()
    {
        if (float.IsNaN(mouseDelta.x) || float.IsNaN(mouseDelta.y))
        {
            return;
        }

        xRotation = Mathf.SmoothDamp(xRotation, xRotation - mouseDelta.y, ref xRotationVelocity, rotationSmoothTime);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(blockVerticalLook ? horizontalLockAngle : xRotation, 0f, 0f);

        float yRotation = Mathf.SmoothDamp(0f, mouseDelta.x, ref yRotationVelocity, rotationSmoothTime);
        if (!blockHorizontalLook)
        {
            playersBody.Rotate(0f, yRotation, 0f);
        }
        else
        {
            playersBody.localRotation = Quaternion.Euler(playersBody.localRotation.eulerAngles.x, verticalLockAngle, playersBody.localRotation.eulerAngles.z);
        }
    }

    private void OnTimeScaleChanged()
    {
        if (Time.timeScale == 0)
        {
            xRotationVelocity = 0f;
            yRotationVelocity = 0f;
        }
        else if (Time.timeScale == 1)
        {
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            xRotationVelocity = 0f;
            yRotationVelocity = 0f;
        }
    }
}
