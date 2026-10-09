
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("Input")]
    [SerializeField] private bool enableKeyboardInput = true;

    private CharacterController controller;
    private Vector2 moveInput;
    private float verticalVelocity;
    private bool runRequested;
    private bool jumpRequested;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (enableKeyboardInput)
        {
            moveInput = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );

            moveInput = Vector2.ClampMagnitude(moveInput, 1f);

            runRequested = Input.GetKey(KeyCode.LeftShift);

            if (Input.GetButtonDown("Jump"))
                jumpRequested = true;
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 forward;
        Vector3 right;

        if (cameraTransform != null)
        {
            forward = cameraTransform.forward;
            right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();
        }
        else
        {
            forward = Vector3.forward;
            right = Vector3.right;
        }

        Vector3 direction =
            forward * moveInput.y +
            right * moveInput.x;

        direction = Vector3.ClampMagnitude(direction, 1f);

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (jumpRequested && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );
        }

        jumpRequested = false;
        verticalVelocity += gravity * Time.deltaTime;

        float speed = runRequested ? runSpeed : walkSpeed;

        Vector3 velocity = direction * speed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    // Mobile controls can call these methods.
    public void SetMoveInput(Vector2 input)
    {
        moveInput = Vector2.ClampMagnitude(input, 1f);
    }

    public void SetRunning(bool running)
    {
        runRequested = running;
    }

    public void RequestJump()
    {
        jumpRequested = true;
    }

    public void StopMovement()
    {
        moveInput = Vector2.zero;
        runRequested = false;
    }
}
