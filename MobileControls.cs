
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControls : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private CharacterController player;
    [SerializeField] private Transform cameraTransform;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    private Vector2 movementInput;
    private float verticalVelocity;
    private bool runHeld;
    private bool jumpRequested;

    public void SetMovementInput(Vector2 input)
    {
        movementInput = Vector2.ClampMagnitude(input, 1f);
    }

    public void SetRunHeld(bool isHeld)
    {
        runHeld = isHeld;
    }

    public void Jump()
    {
        jumpRequested = true;
    }

    public void StopMovement()
    {
        movementInput = Vector2.zero;
        runHeld = false;
    }

    private void Update()
    {
        if (player == null)
            return;

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

        Vector3 moveDirection =
            forward * movementInput.y +
            right * movementInput.x;

        moveDirection = Vector3.ClampMagnitude(
            moveDirection, 1f
        );

        float speed = runHeld ? runSpeed : moveSpeed;

        if (player.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (jumpRequested && player.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );
        }

        jumpRequested = false;
        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity =
            moveDirection * speed;

        velocity.y = verticalVelocity;

        player.Move(velocity * Time.deltaTime);

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            player.transform.rotation =
                Quaternion.Slerp(
                    player.transform.rotation,
                    Quaternion.LookRotation(moveDirection),
                    10f * Time.deltaTime
                );
        }
    }

    // Connect these methods to Unity UI buttons.
    public void RunButtonDown()
    {
        SetRunHeld(true);
    }

    public void RunButtonUp()
    {
        SetRunHeld(false);
    }

    public void JumpButtonPressed()
    {
        Jump();
    }
}
