using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputManager : InputManager
{
    private PlayerInput m_PlayerInput;

    [HideInInspector] public bool m_ShiftLockPressed;
    [HideInInspector] public bool m_MoveCameraHeld;
    
    void Start()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
    }

    // Update is handled in the base class

    protected override void HandleInputs()
    {
        m_Movement = m_PlayerInput.actions["Move"].ReadValue<Vector2>();
        m_Look = m_PlayerInput.actions["Look"].ReadValue<Vector2>();
        m_JumpPressed = m_PlayerInput.actions["Jump"].WasPressedThisFrame();
        m_JumpHeld = m_PlayerInput.actions["Jump"].IsPressed();
        m_JumpReleased = m_PlayerInput.actions["Jump"].WasReleasedThisFrame();
        m_SprintHeld = m_PlayerInput.actions["Sprint"].IsPressed();
        m_FireHeld = m_PlayerInput.actions["Attack"].IsPressed();
        m_ShiftLockPressed = m_PlayerInput.actions["ShiftLock"].WasPressedThisFrame();
        m_MoveCameraHeld = m_PlayerInput.actions["MoveCamera"].IsPressed();
    }
}
