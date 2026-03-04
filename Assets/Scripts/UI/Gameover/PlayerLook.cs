using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;      // the player (rotates yaw)
    public Transform cameraTransform; // usually same object as this script

    [Header("Look")]
    public float mouseSensitivity = 150f;
    public bool canLook = true;

    private float xRotation = 0f;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canLook) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

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