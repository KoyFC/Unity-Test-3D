using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthScript))]
[RequireComponent(typeof(PlayerMovementScript))]
[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    internal HealthScript m_HealthScript;
    internal PlayerMovementScript m_MovementScript;
    internal PlayerInputManager m_InputManager;

    internal Rigidbody m_Rigidbody;
    #endregion

    #region Main Methods
    void Awake()
    {
        GetAllComponents();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

    #region Helper Methods
    private void GetAllComponents()
    {
        m_HealthScript = GetComponent<HealthScript>();
        m_MovementScript = GetComponent<PlayerMovementScript>();
        m_InputManager = GetComponent<PlayerInputManager>();

        m_Rigidbody = GetComponent<Rigidbody>();
    }
    #endregion
}
