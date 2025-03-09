using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset controls;
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float rotationSpeed = 200.0f;
    public float gravity = 9.8f;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private Rigidbody rb;
    private bool isGrounded;

    public Collider groundCheckCollider;
    public LayerMask groundLayer;

    private Vector2 lookInput; // Stores mouse input for rotation
    private Vector2 moveInput; // Stores movement input

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Get input actions from the InputActionAsset
        moveAction = controls.FindAction("Move");
        lookAction = controls.FindAction("Look");
        jumpAction = controls.FindAction("Jump");

        controls.Enable();
    }

    void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        lookAction.performed += OnLook;
        lookAction.canceled += OnLook;

        jumpAction.performed += OnJump;
    }

    void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        lookAction.performed -= OnLook;
        lookAction.canceled -= OnLook;

        jumpAction.performed -= OnJump;

        controls.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Update()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheckCollider.transform.position, 0.1f, groundLayer);

        // Rotate the robot based on mouse input
        RotatePlayer();
    }

    void FixedUpdate()
    {
        // Apply movement based on the current forward direction
        MovePlayer();

        // Apply gravity manually if not grounded
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    void RotatePlayer()
    {
        // Rotate the robot using mouse input
        if (lookInput.sqrMagnitude > 0.01f)
        {
            float rotationX = lookInput.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationX, 0);
        }
    }

    void MovePlayer()
    {
        // Move the robot forward based on its local forward direction
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = transform.TransformDirection(direction);
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0); // Stop horizontal movement if no input
        }
    }
}
