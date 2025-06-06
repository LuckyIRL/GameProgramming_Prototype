using System;
using Unity.VisualScripting;
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
    [SerializeField] private float gravity = 30f;
    public bool isGrounded;
    public Collider groundCollider;
    private float verticalVelocity = 0f;

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

        // Set the player's position as the spawn point transform
        if (gameManager != null && gameManager.soGameManager != null)
        {
            // Set the player's position to the spawn point
            transform.position = gameManager.soGameManager.gameStatus.playerPosition.position;
        }
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
        //gameManager.soGameManager.gameStatus.playerPosition = transform.position;

        if (SoundManager.Instance != null)
        {
            // Only make noise if the robot is moving
            bool isMoving = playerInput != Vector2.zero;

            if (isMoving)
            {
                float movementNoise = isRunning ? 1.2f : 0.6f; // Running makes more noise
                SoundManager.Instance.MakeNoise(movementNoise, transform.position);
            }
            else
            {
                // Optional: clear noise when idle
                SoundManager.Instance.MakeNoise(0f, Vector3.zero);
            }
        }

    }

    void Update()
    {
        PlayerMovement();
        ApplyGravity();
        GroundCheck();
    }

    private void ApplyGravity()
    {
        if (isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 gravityVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(gravityVector * Time.deltaTime);
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