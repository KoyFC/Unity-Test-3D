using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [Header("Health")]
    [SerializeField][Min(0)] private int m_StartingHealth = 80;
    [SerializeField][Min(0)] private int m_MaxHealth = 100;
    [Min(0)] public int m_CurrentHealth;

    [Header("Regeneration")]
    public bool m_CanRegen = true;
    public bool m_StartRegen = true;
    [SerializeField][Min(0)] private int m_RegenRate = 1;
    [SerializeField][Min(0)] private float m_RegenDelay = 1f;

    [Header("Invincibility")]
    private Color m_OriginalColor;
    [SerializeField] private Color m_FlashingColor = Color.white;
    [SerializeField][Min(0)] private float m_FlashDuration = 0.1f;
    [SerializeField][Min(0)] private float m_InvincibilityTime = 0.5f;
    
    void Start()
    {
        m_CurrentHealth = m_StartingHealth;
        m_OriginalColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // Regenerate health while m_CanRegen is true and we told it to keep regening
        if (m_CanRegen && m_StartRegen && m_CurrentHealth < m_MaxHealth)
        {
            m_StartRegen = false;
            Invoke(nameof(RegenHealth), m_RegenDelay);
        }
    }
    
    private void RegenHealth()
    {
        // We stop regenerating health if we're dead. Otherwise we keep going.
        if (m_CurrentHealth > 0)
        {
            m_StartRegen = true;
        }
        else
        {
            m_StartRegen = false;
            return;
        }

        m_CurrentHealth += m_RegenRate;
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth, 0, m_MaxHealth);
    }
}
