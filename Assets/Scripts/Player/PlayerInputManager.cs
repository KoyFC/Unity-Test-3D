using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput m_PlayerInput;

    internal Vector2 m_Movement;
    internal Vector2 m_Look;
    internal bool m_JumpPressed;
    internal bool m_SprintHeld;
    
    void Start()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        m_Movement = m_PlayerInput.actions["Move"].ReadValue<Vector2>();
        m_Look = m_PlayerInput.actions["Look"].ReadValue<Vector2>();
        m_JumpPressed = m_PlayerInput.actions["Jump"].WasPressedThisFrame();
        m_SprintHeld = m_PlayerInput.actions["Sprint"].IsPressed();
    }
}
