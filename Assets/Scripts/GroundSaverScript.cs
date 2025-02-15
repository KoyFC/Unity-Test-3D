#define SPAWN_MARKER
using UnityEngine;

public class GroundSaverScript : MonoBehaviour
{
    [SerializeField] private float m_SaveFrequency = 5.0f;
    [SerializeField] private GameObject m_MarkerPrefab;

#if SPAWN_MARKER
    private GameObject m_LastPositionMarker;
#endif

    private Vector3 m_LastGroundPosition;

    private bool m_IsGrounded;

    void Start()
    {
        m_LastGroundPosition = transform.position;

#if SPAWN_MARKER
        m_LastPositionMarker = Instantiate(m_MarkerPrefab, m_LastGroundPosition, m_MarkerPrefab.transform.rotation);
#endif

        InvokeRepeating("SaveGround", 3f, m_SaveFrequency);
    }

    void Update()
    {
        m_IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void SaveGround()
    {
        if (m_IsGrounded)
        {
            m_LastGroundPosition = transform.position;

#if SPAWN_MARKER
            m_LastPositionMarker.transform.position = m_LastGroundPosition;
#endif

            Debug.Log("Ground saved at: " + m_LastGroundPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            transform.position = m_LastGroundPosition;
        }
    }
}
