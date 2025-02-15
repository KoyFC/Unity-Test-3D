using UnityEngine;

[RequireComponent(typeof(HealthScript))]
[RequireComponent(typeof(PlayerMovementScript))]
[RequireComponent(typeof(PlayerInputManager))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    internal HealthScript m_HealthScript;
    internal PlayerMovementScript m_MovementScript;
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
    }
    #endregion
}
