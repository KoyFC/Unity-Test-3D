using UnityEngine;

[RequireComponent(typeof(HealthScript))]
[RequireComponent(typeof(GroundSaverScript))]
public abstract class EntityController : MonoBehaviour
{
    #region Variables
    [HideInInspector] public HealthScript m_HealthScript = null;
    [HideInInspector] public GroundSaverScript m_GroundSaverScript = null;
    #endregion

    #region Main Methods
    void Awake()
    {
        GetAllComponents();
    }
    #endregion

    #region Helper Methods
    protected virtual void GetAllComponents()
    {
        m_HealthScript = GetComponent<HealthScript>();
        m_GroundSaverScript = GetComponent<GroundSaverScript>();
    }
    #endregion
}
