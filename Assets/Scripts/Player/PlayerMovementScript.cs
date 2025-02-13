using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMovementScript : MonoBehaviour
{
    #region Variables
    private Rigidbody m_Rigidbody;
    private PlayerController m_PlayerController;
    private Transform m_CameraTransform;

    [Header("Movement")]
    [SerializeField] private float m_WalkingSpeed = 6.5f;
    [SerializeField] private float m_RunningSpeedMultiplier = 2f;

    [Header("Jump")]
    [SerializeField] private float m_JumpForce = 8f;
    [SerializeField] private float m_CoyoteTime = 0.5f;
    [SerializeField] private float m_JumpBufferTime = 0.5f;
    private bool m_CoyoteTimeActive;
    private bool m_JumpBuffered;

    [Header("Rotation")]
    [SerializeField] private float m_RotationSpeed = 40f;
    internal bool ShiftLock { get; set; }

    private bool IsGrounded { get; set; }

    #endregion

    #region Main Methods
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerController = GetComponent<PlayerController>();
        m_CameraTransform = Camera.main.transform;

        ShiftLock = true;
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (IsGrounded && !m_CoyoteTimeActive)
        {
            StartCoroutine(CoyoteTimeCoroutine());
        }

        if (m_PlayerController.m_InputManager.m_JumpPressed && !m_JumpBuffered)
        {
            StartCoroutine(JumpBufferCoroutine());
        }

        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
    #endregion

    #region Handling Methods
    private void HandleMovement()
    {
        float currentSpeed = m_PlayerController.m_InputManager.m_SprintHeld ?
            m_RunningSpeedMultiplier * m_WalkingSpeed : m_WalkingSpeed;

        Vector2 inputMovement = m_PlayerController.m_InputManager.m_Movement;

        // Determining where to move based on where the camera is looking
        Vector3 moveDirection = m_CameraTransform.right.normalized * inputMovement.x +
            m_CameraTransform.forward.normalized * inputMovement.y;

        m_Rigidbody.linearVelocity = new Vector3(
            moveDirection.x * currentSpeed,
            m_Rigidbody.linearVelocity.y,
            moveDirection.z * currentSpeed);
    }

    private void HandleRotation()
    {
        if (ShiftLock) // Rotating based on the camera direction
        {
            Vector3 cameraDirection = m_CameraTransform.forward;
            cameraDirection.y = 0;

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(cameraDirection),
                1.5f * m_RotationSpeed * Time.deltaTime);
        }
        else // Rotating based on the movement input
        {
            Vector2 inputMovement = m_PlayerController.m_InputManager.m_Movement;

            Vector3 rotateDirection = m_CameraTransform.right.normalized * inputMovement.x +
                m_CameraTransform.forward.normalized * inputMovement.y;

            rotateDirection.y = 0;

            if (inputMovement != Vector2.zero)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(rotateDirection),
                    2 * m_RotationSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleJump()
    {
        if (m_CoyoteTimeActive && m_JumpBuffered)
        {
            m_JumpBuffered = false;
            m_CoyoteTimeActive = false;

            // Clearing the y velocity to avoid taking into account current vertical velocity
            m_Rigidbody.linearVelocity = new Vector3(
                m_Rigidbody.linearVelocity.x,
                0,
                m_Rigidbody.linearVelocity.z);

            // Adding the jump force: if the jump is being held during the first frame, we jump higher
            if (m_PlayerController.m_InputManager.m_JumpHeld)
            {
                m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            }
            else // This helps when buffering the jump
            {
                m_Rigidbody.AddForce(0.5f * m_JumpForce * Vector3.up, ForceMode.Impulse);
            }
        }

        // Reducing the jump height if the jump button is released
        if (m_PlayerController.m_InputManager.m_JumpReleased && m_Rigidbody.linearVelocity.y > 0)
        {
            m_Rigidbody.linearVelocity = new Vector3(
                m_Rigidbody.linearVelocity.x,
                m_Rigidbody.linearVelocity.y * 0.5f,
                m_Rigidbody.linearVelocity.z);
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator CoyoteTimeCoroutine()
    {
        m_CoyoteTimeActive = true;
        yield return new WaitForSeconds(m_CoyoteTime);
        m_CoyoteTimeActive = false;
    }

    private IEnumerator JumpBufferCoroutine()
    {
        m_JumpBuffered = true;
        yield return new WaitForSeconds(m_JumpBufferTime);
        m_JumpBuffered = false;
    }
    #endregion
}
