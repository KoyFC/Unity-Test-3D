//#define ROTATE_CAMERA
#define ROTATE_MOVEMENT
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    #region Variables
    private PlayerController m_PlayerController;
    private Transform m_CameraTransform;

    [Header("Movement")]
    [SerializeField] private float m_WalkingSpeed = 6.5f;
    [SerializeField] private float m_RunningSpeedMultiplier = 2f;

    [Header("Jump")]
    [SerializeField] private float m_JumpForce = 8f;
    public bool m_CanJump;
    public bool m_CoyoteTimeActive;
    public bool m_JumpBuffered;

    [Header("Rotation")]
    [SerializeField] private float m_RotationSpeed = 40f;

    private bool IsGrounded { get; set; }
    #endregion

    #region Main Methods
    void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
        m_CameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

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

        m_PlayerController.m_Rigidbody.linearVelocity = new Vector3(
            moveDirection.x * currentSpeed, 
            m_PlayerController.m_Rigidbody.linearVelocity.y,
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
        if (IsGrounded && m_PlayerController.m_InputManager.m_JumpPressed)
        {
            m_PlayerController.m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }

        if (m_PlayerController.m_InputManager.m_JumpReleased && m_PlayerController.m_Rigidbody.linearVelocity.y > 0)
        {
            m_PlayerController.m_Rigidbody.linearVelocity = new Vector3(
                m_PlayerController.m_Rigidbody.linearVelocity.x,
                m_PlayerController.m_Rigidbody.linearVelocity.y * 0.5f,
                m_PlayerController.m_Rigidbody.linearVelocity.z);
        }
    }
    #endregion

    #region Helper Methods

    #endregion
}
