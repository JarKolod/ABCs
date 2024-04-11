using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    private CharacterController playerCharController;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float speed = 2.0f;
    private float runSpeed = 3.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;


    private void Start()
    {
        playerCharController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        inputManager.inputMaster.Movement.Jump.started += _ => PlayerJump();
    }

    private void OnEnable()
    {
        
    }

    void Update()
    {

    }

    private void PlayerJump()
    {

    }
}