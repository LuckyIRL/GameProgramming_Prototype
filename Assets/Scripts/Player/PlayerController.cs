using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    RobotInputActions robotInputActions;
    private Vector2 playerInput;
    [SerializeField] CharacterController controller;
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private float playerRotation = 200.0f;
    [SerializeField] private float gravity = 9.8f;
    public bool isGrounded;
    public Collider groundCollider;

    //setup a variable to point to the Animator Controller for the character]
    private Animator animator;
    private float currentSpeed;
    private bool isRunning;
    //and another variable to hold the input value
    float vertInput;
    float horizontalInput;

    private NavMeshAgent agent;

    private void Start()
    {
        //get the Animator Controller Component from the character component hierarchy
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    private void PlayerMovement()
    {
        // Determine current speed (running or walking)
        currentSpeed = isRunning ? runSpeed : walkSpeed;


        // Move and rotate player
        controller.Move(transform.forward * playerInput.y * currentSpeed * Time.deltaTime);
        transform.Rotate(transform.up, playerRotation * playerInput.x * Time.deltaTime);
    }

    void Update()
    {
        PlayerMovement();
        ApplyGravity();
        GroundCheck();

        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        //// Set the vAxisInput parameter for animation (use absolute to handle backwards movement too)
        //float movementAmount = Mathf.Abs(playerInput.y);
        //animator.SetFloat("vAxisInput", movementAmount);

        //// Set the hAxisInput parameter for animation
        //animator.SetFloat("hAxisInput", playerInput.x);

        //// Set running flag for animation
        //animator.SetBool("isRunning", isRunning);

        //// Check for running input (Shift key)
        //isRunning = Keyboard.current.leftShiftKey.isPressed; ;
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            controller.Move(Vector3.down * gravity * Time.deltaTime);
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

    void FixedUpdate()
    {
        //use fixed update to control the animation as it will behave in real time
        //now set the animator float value (vAxisInput) with the input value
        //animator.SetFloat("vAxisInput", vertInput);

        //animator.SetFloat("hAxisInput", horizontalInput);
        //// Detect Z Key press
        //if (Input.GetKey(KeyCode.Z))
        //{
        //    // Set runBool to true if pressed
        //    animator.SetBool("runBool", true);
        //    Debug.Log("Run");
        //}
        //else
        //{
        //    // Set runBool to false if not pressed
        //    animator.SetBool("runBool", false);
        //    Debug.Log("No Run");
        //}
        // Detect X Key press
        if (Input.GetKey(KeyCode.X))
        {
            // Set the Crouch Layer Weight to 0.5, this
            // activtes the masked couch animation
            animator.SetLayerWeight(1, 0.5f);
        }
        else
        {
            // Set the Couch Layer Weight back to 0.0
            // This deactivated the crouch animation
            animator.SetLayerWeight(1, 0.0f);
        }
    }
}
