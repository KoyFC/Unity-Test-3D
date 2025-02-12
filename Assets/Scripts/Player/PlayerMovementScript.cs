//#define ROTATE_CAMERA
#define ROTATE_MOVEMENT
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
    public bool m_CoyoteTimeActive;
    public bool m_JumpBuffered;
    private float m_CoyoteTimeCounter;
    private float m_JumpBufferCounter;

    [Header("Rotation")]
    [SerializeField] private float m_RotationSpeed = 40f;

    private bool IsGrounded { get; set; }
    #endregion

    #region Main Methods
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerController = GetComponent<PlayerController>();
        m_CameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        HandleCoyoteTime();
        HandleJumpBuffer();
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
#if ROTATE_CAMERA
        Vector3 cameraDirection = m_CameraTransform.forward;
        cameraDirection.y = 0;

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            Quaternion.LookRotation(cameraDirection), 
            m_RotationSpeed * Time.deltaTime);

#elif ROTATE_MOVEMENT
        Vector2 inputMovement = m_PlayerController.m_InputManager.m_Movement;

        // Rotating based on the movement input
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
#endif
    }

    private void HandleJump()
    {
        if (m_CoyoteTimeActive && m_JumpBuffered)
        {
            m_JumpBufferCounter = 0;

            m_Rigidbody.linearVelocity = new Vector3(
                m_Rigidbody.linearVelocity.x,
                0,
                m_Rigidbody.linearVelocity.z);

            if (m_PlayerController.m_InputManager.m_JumpHeld)
            {
                m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
            }
            else
            {
                m_Rigidbody.AddForce(0.5f * m_JumpForce * Vector3.up, ForceMode.Impulse);
            }
                
        }

        if (m_PlayerController.m_InputManager.m_JumpReleased && m_Rigidbody.linearVelocity.y > 0)
        {
            m_CoyoteTimeCounter = 0;

            m_Rigidbody.linearVelocity = new Vector3(
                m_Rigidbody.linearVelocity.x,
                m_Rigidbody.linearVelocity.y * 0.5f,
                m_Rigidbody.linearVelocity.z);
        }
    }

    private void HandleCoyoteTime()
    {
        if (IsGrounded)
        {
            m_CoyoteTimeCounter = m_CoyoteTime;
        }
        else
        {
            m_CoyoteTimeCounter -= Time.deltaTime;
        }


        if (m_CoyoteTimeCounter > 0)
        {
            m_CoyoteTimeActive = true;
        }
        else
        {
            m_CoyoteTimeCounter = 0;
            m_CoyoteTimeActive = false;
        }
    }

    private void HandleJumpBuffer()
    {
        if (m_PlayerController.m_InputManager.m_JumpPressed)
        {
            m_JumpBufferCounter = m_JumpBufferTime;
        }
        else
        {
            m_JumpBufferCounter -= Time.deltaTime;
        }

        if (m_JumpBufferCounter > 0)
        {
            m_JumpBuffered = true;
        }
        else
        {
            m_JumpBufferCounter = 0;
            m_JumpBuffered = false;
        }
    }
    #endregion

    #region Helper Methods
    
    #endregion
}
