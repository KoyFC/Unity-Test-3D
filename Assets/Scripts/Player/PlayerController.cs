using UnityEngine;

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
    #endregion

    #region Main Methods
    void Awake()
    {
        GetAllComponents();
    }

    #endregion

    #region Helper Methods
    private void GetAllComponents()
    {
        m_HealthScript = GetComponent<HealthScript>();
        m_MovementScript = GetComponent<PlayerMovementScript>();
        m_InputManager = GetComponent<PlayerInputManager>();
    }
    #endregion
}
