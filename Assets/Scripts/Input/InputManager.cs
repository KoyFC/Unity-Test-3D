using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public Vector2 m_Look;

    [HideInInspector] public Vector2 m_Movement;
    [HideInInspector] public bool m_SprintPressed;

    [HideInInspector] public bool m_JumpPressed;
    [HideInInspector] public bool m_JumpHeld;
    [HideInInspector] public bool m_JumpReleased;

    [HideInInspector] public bool m_FireHeld;
    [HideInInspector] public bool m_ReloadPressed;
    [HideInInspector] public float m_MouseWheel;
    
    void Update()
    {
        HandleInputs();
    }

    protected virtual void HandleInputs() { }
}
