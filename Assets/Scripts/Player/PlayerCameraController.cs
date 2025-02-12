using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    PlayerController m_PlayerController;
    [SerializeField] CinemachineInputAxisController m_InputAxisController;
    void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();

        m_InputAxisController.enabled = m_PlayerController.m_MovementScript.ShiftLock;
    }

    void Update()
    {
        bool shiftLock = m_PlayerController.m_MovementScript.ShiftLock;
        bool shiftLockPressed = m_PlayerController.m_InputManager.m_ShiftLockPressed;
        bool moveCameraHeld = m_PlayerController.m_InputManager.m_MoveCameraHeld;

        if (shiftLockPressed)
        {
            m_InputAxisController.enabled = shiftLock;
        }
        else if (!shiftLock && moveCameraHeld)
        {
            m_InputAxisController.enabled = true;
        }
        else if (!shiftLock && !moveCameraHeld)
        {
            m_InputAxisController.enabled = false;
        }
    }

}
