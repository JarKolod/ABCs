using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController playerCharController;

    public bool AllowMoveLeft { get => allowMoveLeft; set => allowMoveLeft = value; }
    public bool AllowMoveRight { get => allowMoveRight; set => allowMoveRight = value; }
    public bool AllowMoveForward { get => allowMoveForward; set => allowMoveForward = value; }
    public bool AllowMoveBack { get => allowMoveBack; set => allowMoveBack = value; }
    public bool AllowOnlySpecifiedAxisMovement { get => allowOnlySpecifiedAxisMovement; set => allowOnlySpecifiedAxisMovement = value; }

    [Header("Dependencies")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] float directionalMoveSmoothTime = 0.05f;
    [Space]
    [Header("Player Parameters")]
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] int maxJumpCharges = 2;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float characterGravityScalar = 1.7f;
    [Space]
    [Header("Move Disabling")]
    [SerializeField] bool allowOnlySpecifiedAxisMovement = false;
    [SerializeField] bool reverseHorizontalAxis = false;
    [SerializeField] bool reverseVerticalAxis = false;
    [Space]
    [SerializeField] bool allowMoveLeft = true;
    [SerializeField] bool allowMoveRight = true;
    [SerializeField] bool allowMoveForward = true;
    [SerializeField] bool allowMoveBack = true;
    [Space]
    [SerializeField] bool allowJump = true;
    [Space]
    [Header("Move Player")]
    [SerializeField] Vector2 playerInputDirections;

    Vector3 playerDirectionalMove;
    Vector3 directionalSmoothVelocity = Vector3.zero;
    float currentYFrameVelocity;
    float prevYFrameVelocity;
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

        ApplyDirectionalMovementRestrictions();

        Vector3 desiredDirectionalMove = new Vector3();
        if(allowOnlySpecifiedAxisMovement)
        {
            // - because player character is facing opposite direction
            desiredDirectionalMove = 
                (reverseVerticalAxis ? -1 : 1) * Vector3.forward * playerInputDirections.y + 
                (reverseHorizontalAxis ? -1 : 1) * Vector3.right * playerInputDirections.x;
        }
        else
        {
            desiredDirectionalMove = transform.forward * playerInputDirections.y + transform.right * playerInputDirections.x;
        }

        playerDirectionalMove = Vector3.SmoothDamp(playerDirectionalMove, desiredDirectionalMove, ref directionalSmoothVelocity, directionalMoveSmoothTime);

        playerCharController.Move(Time.deltaTime * (playerDirectionalMove * speed + currentYFrameVelocity * Vector3.up));

        prevYFrameVelocity = currentYFrameVelocity;
    }

    private void FixedUpdate()
    {
        if (playerCharController.isGrounded)
        {
            onGrounded();
        }
    }

    private void ApplyDirectionalMovementRestrictions()
    {
        if (!allowMoveForward && playerInputDirections.y > 0)
        {
            playerInputDirections.y = 0;
        }
        if (!allowMoveBack && playerInputDirections.y < 0)
        {
            playerInputDirections.y = 0;
        }
        if (!allowMoveLeft && playerInputDirections.x < 0)
        {
            playerInputDirections.x = 0;
        }
        if (!allowMoveRight && playerInputDirections.x > 0)
        {
            playerInputDirections.x = 0;
        }

    }

    private void OnJumpBtn(InputAction.CallbackContext ctx)
    {
        float initialJumpAcceleration = Mathf.Sqrt(jumpHeight * 3f * -gravityValue);

        if (currentJumpCharges > 0 && allowJump)
        {
            --currentJumpCharges;
            currentYFrameVelocity = initialJumpAcceleration;
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