using UnityEngine;

[RequireComponent(typeof(PlayerMovementScript))]
[RequireComponent(typeof(PlayerInputManager))]
public class PlayerController : EntityController
{
    #region Variables
    internal PlayerMovementScript m_MovementScript = null;
    #endregion

    #region Helper Methods
    protected override void GetAllComponents()
    {
        base.GetAllComponents();
        m_MovementScript = GetComponent<PlayerMovementScript>();
    }
    #endregion
}
