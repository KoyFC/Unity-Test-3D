//#define USING_RIGHTCLICK
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

        Cursor.lockState = Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        bool shiftLock = m_PlayerController.m_MovementScript.ShiftLock;
        bool shiftLockPressed = m_PlayerController.m_InputManager.m_ShiftLockPressed;
        bool moveCameraHeld = m_PlayerController.m_InputManager.m_MoveCameraHeld;

        if (shiftLockPressed)
        {
            shiftLock = !shiftLock;
            m_PlayerController.m_MovementScript.ShiftLock = shiftLock;
        }

#if USING_RIGHTCLICK
        if (shiftLockPressed)
        {
            m_InputAxisController.enabled = shiftLock;

            Cursor.lockState = shiftLock ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !shiftLock;
        } 
        else if (!shiftLock && moveCameraHeld)
        {
            m_InputAxisController.enabled = true;
        }
        else if (!shiftLock && !moveCameraHeld)
        {
            m_InputAxisController.enabled = false;
        }
#else
        if (shiftLockPressed)
        {
            m_InputAxisController.enabled = shiftLock;
        }
        else if (!shiftLock)
        {
            m_InputAxisController.enabled = true;
        }
#endif
    }

}
