using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // This method is automatically called by PlayerInput component
    // when movement input is detected — the name must match exactly
    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("Moving: " + movement); // add this line
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
