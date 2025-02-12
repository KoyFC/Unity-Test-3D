using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private enum States
    {
        Idle,
        Walking,
        Running,
        Jumping
    }

    private PlayerController m_PlayerController;
    private Transform m_CameraTransform;

    [Header("Movement")]
    [SerializeField] private float m_WalkingSpeed = 6.5f;
    [SerializeField] private float m_RunningSpeedMultiplier = 2f;
    [SerializeField] private float m_JumpForce = 8f;

    [Header("Rotation")]
    [SerializeField] private float m_RotationSpeed = 40f;

    private bool IsGrounded { get; set; }
    private bool m_IsGrounded;

    private States m_CurrentState = States.Idle;

    void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
        m_CameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleRotation();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float appliedMultiplier = m_PlayerController.m_InputManager.m_SprintHeld ? m_RunningSpeedMultiplier : 1f;

        Vector2 inputMovement = m_PlayerController.m_InputManager.m_Movement;

        // Determining where to move based on where the camera is looking
        Vector3 moveDirection = m_CameraTransform.right.normalized * inputMovement.x +
                                m_CameraTransform.forward.normalized * inputMovement.y;

        m_PlayerController.m_Rigidbody.linearVelocity = appliedMultiplier * m_WalkingSpeed * new Vector3(moveDirection.x, 0, moveDirection.z);
    }

    private void HandleRotation()
    {
        Vector3 cameraDirection = m_CameraTransform.forward;
        cameraDirection.y = 0;

        
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            Quaternion.LookRotation(cameraDirection), 
            m_RotationSpeed * Time.deltaTime);
        
    }

    private void HandleJump()
    {

    }
}
