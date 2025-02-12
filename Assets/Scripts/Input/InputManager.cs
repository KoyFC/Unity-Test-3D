using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 m_Movement;
    public Vector2 m_Look;
    public bool m_JumpPressed;
    public bool m_JumpHeld;
    public bool m_JumpReleased;
    public bool m_SprintHeld;
    public bool m_FireHeld;

    void Update()
    {
        HandleInputs();
    }

    protected virtual void HandleInputs() { }
}
