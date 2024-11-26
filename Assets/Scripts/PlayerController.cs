using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    RobotInputActions robotInputActions;
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerRotation = 200.0f;
    [SerializeField] private float gravity = 9.8f;
    public bool isGrounded;
    public bool isSprinting;
    public Collider groundCollider;

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
        GroundCheck();
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

    private void OnAiming() {
   
        robotInputActions.isAiming = true;
        Debug.Log("Aiming");
    }
}
