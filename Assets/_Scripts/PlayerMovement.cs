using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController playerCharController;

    [SerializeField] private InputManager inputManager;
    [SerializeField] float directionalMoveSmoothTime = 0.05f;
    [Space]
    [SerializeField] float speed = 2.0f;
    [SerializeField] float runSpeed = 3.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] int maxJumpCharges = 2;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float characterGravityScalar = 1.7f;
    [Space]

    Vector3 playerDirectionalMove;
    Vector3 directionalSmoothVelocity = Vector3.zero;

    float currentYFrameVelocity;
    float prevYFrameVelocity;
    Vector3 playerInputDirections;
    private int currentJumpCharges;
    private float currentYVelocity;

    Action onGrounded;

    private void Awake()
    {
        playerCharController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        SetupMovementActions();

        onGrounded += () => currentJumpCharges = maxJumpCharges;
        onGrounded += ApplyNegativeVelocity;

        currentJumpCharges = maxJumpCharges;
    }

    private void SetupMovementActions()
    {
        inputManager.inputMaster.Movement.Jump.started += OnJumpBtn;
        inputManager.inputMaster.Movement.Direction.started += OnMove;
        inputManager.inputMaster.Movement.Direction.performed += OnMove;
        inputManager.inputMaster.Movement.Direction.canceled += OnMove;
    }

    private void Update()
    {
        ApplyGravity();

        if (playerCharController.isGrounded)
        {
            onGrounded();
        }

        Vector3 desiredDirectionalMove = transform.forward * playerInputDirections.y + transform.right * playerInputDirections.x;
        playerDirectionalMove = Vector3.SmoothDamp(playerDirectionalMove, desiredDirectionalMove, ref directionalSmoothVelocity, directionalMoveSmoothTime);

        playerCharController.Move(Time.deltaTime * (playerDirectionalMove * speed + currentYFrameVelocity * Vector3.up));

        prevYFrameVelocity = currentYFrameVelocity;
    }

    private void OnJumpBtn(InputAction.CallbackContext ctx)
    {
        float initialJumpAcceleration = Mathf.Sqrt(jumpHeight * 3f * -gravityValue);

        if (currentJumpCharges > 0)
        {
            currentYFrameVelocity += initialJumpAcceleration;
            currentJumpCharges--;
        }
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        playerInputDirections = ctx.ReadValue<Vector2>();
    }

    private void ApplyGravity()
    {
        if (!playerCharController.isGrounded)
        {
            currentYFrameVelocity += characterGravityScalar * gravityValue * Time.deltaTime;
        }
    }

    private void ApplyNegativeVelocity()
    {
        if (currentYFrameVelocity <= 0f)
        {
            currentYFrameVelocity = -1f;
            return;
        }
    }

}