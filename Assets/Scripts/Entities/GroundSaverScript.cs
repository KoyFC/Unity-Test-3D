using System.Collections;
using UnityEngine;

public class GroundSaverScript : MonoBehaviour
{
    [Header("Ground Saver Settings")]
    [SerializeField] private float m_SaveFrequency = 5.0f;
    [SerializeField] private bool m_SavePosition = true;
    private bool m_KeepSaving;
    private Vector3 m_LastGroundPosition;

    [Header("Marker Settings")]
    [SerializeField] private GameObject m_MarkerPrefab;
    [SerializeField] private bool m_SpawnMarker = false;
    private GameObject m_LastPositionMarker;

    public bool IsGrounded { get; private set; }

    void Start()
    {
        m_KeepSaving = m_SavePosition;

        m_LastGroundPosition = transform.position;

        if (m_SpawnMarker)
        {
            m_LastPositionMarker = Instantiate(m_MarkerPrefab, m_LastGroundPosition, m_MarkerPrefab.transform.rotation);
        }

        StartCoroutine(SaveGround());
    }

    void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        // Stop the coroutine if it is not told to save the position
        if (!m_SavePosition && m_KeepSaving)
        {
            m_KeepSaving = false;
        }
        // Reactivate the coroutine if it was stopped and it is told to save the position
        else if (m_SavePosition && !m_KeepSaving)
        {
            m_KeepSaving = true;
            StartCoroutine(SaveGround());
        }
    }

    private IEnumerator SaveGround()
    {
        #if UNITY_EDITOR
        Debug.Log("Ground saving Coroutine started");
        #endif

        while (m_KeepSaving)
        {
            yield return new WaitUntil(() => IsGrounded);

            m_LastGroundPosition = transform.position;

            #if UNITY_EDITOR
            Debug.Log("Ground saved at: " + m_LastGroundPosition);
            #endif
            
            if (m_SpawnMarker)
            {
                m_LastPositionMarker.transform.position = m_LastGroundPosition;
            }

            yield return new WaitForSeconds(m_SaveFrequency);
        }

        #if UNITY_EDITOR
        Debug.Log("Ground saving Coroutine was stopped");
        #endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            transform.position = m_LastGroundPosition;
        }
    }
}
