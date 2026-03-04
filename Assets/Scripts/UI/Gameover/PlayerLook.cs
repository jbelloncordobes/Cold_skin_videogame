using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;      // the player (rotates yaw)
    public Transform cameraTransform; // usually same object as this script

    [Header("Look")]
    public float mouseSensitivity = 150f;
    public bool canLook = true;

    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;

    private float xRotation = 0f;
    private InputAction lookAction;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = transform;

        // Get the PlayerInput component
        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
            lookAction = playerInput.actions["Look"];

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canLook) return;

        // Read look input from the new Input System
        Vector2 lookInput = lookAction != null ? lookAction.ReadValue<Vector2>() : Vector2.zero;
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // Pitch (up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Yaw (left/right)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void SetLookEnabled(bool enabled)
    {
        canLook = enabled;

        Cursor.lockState = enabled ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !enabled;
    }
}