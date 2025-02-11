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

    [Header("Movement")]
    [SerializeField] private float m_WalkingSpeed = 6.5f;
    [SerializeField] private float m_RunningSpeedMultiplier = 2f;
    [SerializeField] private float m_JumpForce = 8f;

    private bool IsGrounded { get; set; }
    private bool m_IsGrounded;

    private States m_CurrentState = States.Idle;

    void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        HandleLook();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        Vector2 movement = m_PlayerController.m_InputManager.m_Movement;

        float appliedMultiplier = m_PlayerController.m_InputManager.m_SprintHeld ? m_RunningSpeedMultiplier : 1f;

        m_PlayerController.m_Rigidbody.linearVelocity = appliedMultiplier * m_WalkingSpeed * new Vector3(movement.x, 0, movement.y);
    }

    private void HandleJump()
    {

    }

    private void HandleLook()
    {

    }

}
