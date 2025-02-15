using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputManager : InputManager
{
    public static PlayerInputManager Instance;

    private PlayerInput m_PlayerInput;

    [HideInInspector] public bool m_ShiftLockPressed;
    [HideInInspector] public bool m_MoveCameraHeld;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        m_PlayerInput = GetComponent<PlayerInput>();
    }

    // Update is handled in the base class

    protected override void HandleInputs()
    {
        m_Look = m_PlayerInput.actions["Look"].ReadValue<Vector2>();
        m_ShiftLockPressed = m_PlayerInput.actions["ShiftLock"].WasPressedThisFrame();
        m_MoveCameraHeld = m_PlayerInput.actions["MoveCamera"].IsPressed();

        m_Movement = m_PlayerInput.actions["Move"].ReadValue<Vector2>();
        m_SprintPressed = m_PlayerInput.actions["Sprint"].WasPressedThisFrame();

        m_JumpPressed = m_PlayerInput.actions["Jump"].WasPressedThisFrame();
        m_JumpHeld = m_PlayerInput.actions["Jump"].IsPressed();
        m_JumpReleased = m_PlayerInput.actions["Jump"].WasReleasedThisFrame();

        m_FireHeld = m_PlayerInput.actions["Attack"].IsPressed();

        if (m_PlayerInput.actions["NextWeapon"].WasPressedThisFrame())
        {
            m_MouseWheel = 1;
        }
        else if (m_PlayerInput.actions["PreviousWeapon"].WasPressedThisFrame())
        {
            m_MouseWheel = -1;
        }
        else
        {
            m_MouseWheel = 0;
        }

        if (m_MouseWheel == 0)
        {
            m_MouseWheel = m_PlayerInput.actions["ScrollWeapon"].ReadValue<float>();

            if (m_MouseWheel != 0)
            {
                m_MouseWheel = m_MouseWheel > 0 ? 1 : -1;
            }
        }
    }
}
