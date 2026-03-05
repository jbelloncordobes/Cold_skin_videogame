using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float gravity = -20f;
    public float jumpHeight = 1.2f;

    [Header("State")]
    public bool canMove = true;

    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;

    private CharacterController controller;
    private float yVelocity = 0f;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Get the PlayerInput component
        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
        }


    }

    void Update()
    {
        if (!canMove) return;
        HandleMovement();
    }

    void HandleMovement()
    {
        // Read movement input from the new Input System
        Vector2 moveInput = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        float moveX = moveInput.x; // A/D
        float moveZ = moveInput.y; // W/S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Horizontal movement
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Stick to ground
        if (controller.isGrounded && yVelocity < 0f)
            yVelocity = -2f;

        // Jump
        if (controller.isGrounded && jumpAction != null && jumpAction.WasPressedThisFrame())
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Gravity
        yVelocity += gravity * Time.deltaTime;

        // Vertical movement
        controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    }
}