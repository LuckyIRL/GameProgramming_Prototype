using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    RobotInputActions robotInputActions;
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] private float baseWalkSpeed = 5.0f;
    [SerializeField] private float baseRunSpeed = 10.0f;
    [SerializeField] private float basePlayerRotation = 200.0f;
    [SerializeField] private float gravity = 9.8f;
    public bool isGrounded;
    public Collider groundCollider;

    // Reference to the GameManager
    private GameManager gameManager;

    //setup a variable to point to the Animator Controller for the character]
    private Animator animator;
    private float currentSpeed;
    private bool isRunning;
    //and another variable to hold the input value
    float vertInput;
    float horizontalInput;

    private void Start()
    {
        // Get the GameManager instance
        gameManager = GameManager.instance;

        // Set the player's position from the saved game status
        transform.position = gameManager.soGameManager.gameStatus.playerPosition;
    }

    private void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    private void PlayerMovement()
    {
        // Determine current speed (running or walking)
        currentSpeed = isRunning ? baseRunSpeed : baseWalkSpeed;

        // Apply move speed upgrade
        currentSpeed += gameManager.soGameManager.gameStatus.moveSpeedUpgrade.level * 1.0f;

        // Move and rotate player
        controller.Move(transform.forward * playerInput.y * currentSpeed * Time.deltaTime);
        transform.Rotate(transform.up, (basePlayerRotation + gameManager.soGameManager.gameStatus.turnSpeedUpgrade.level * 10.0f) * playerInput.x * Time.deltaTime);

        // Update the player's position in the SO_GameManager
        gameManager.soGameManager.gameStatus.playerPosition = transform.position;
    }

    void Update()
    {
        PlayerMovement();
        ApplyGravity();
        GroundCheck();
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            Vector3 gravityVector = Vector3.down * gravity * Time.deltaTime;
            controller.Move(gravityVector);
        }
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.0f))
        {
            isGrounded = true;
            groundCollider = hit.collider;
        }
        else
        {
            isGrounded = false;
            groundCollider = null;
        }
    }
}