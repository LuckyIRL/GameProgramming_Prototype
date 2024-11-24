using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerRotation = 200.0f;
    [SerializeField] private float gravity = 9.8f;

    private void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    private void PlayerMovement()
    {
        // Move the player forward and backward
        controller.Move(transform.forward * playerInput.y * playerSpeed * Time.deltaTime);
        // Move the player left and right
        transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime);
    }

    void Update()
    {
        PlayerMovement();

        // Apply gravity to the player
        controller.Move(Vector3.down * gravity * Time.deltaTime);
    }
}
