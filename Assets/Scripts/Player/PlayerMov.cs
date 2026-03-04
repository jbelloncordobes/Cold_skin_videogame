using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public float gravity = -20f;
    public float jumpHeight = 1.2f;

    [Header("State")]
    public bool canMove = true;

    private CharacterController controller;
    private float yVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!canMove) return;
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D
        float moveZ = Input.GetAxis("Vertical");   // W/S

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Horizontal movement
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Stick to ground
        if (controller.isGrounded && yVelocity < 0f)
            yVelocity = -2f;

        // Jump
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Gravity
        yVelocity += gravity * Time.deltaTime;

        // Vertical movement
        controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    }
}