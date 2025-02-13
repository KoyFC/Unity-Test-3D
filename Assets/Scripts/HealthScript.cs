using System.Collections;
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
    [SerializeField] private GameObject m_ShieldPrefab;
    [SerializeField] private Color m_FlashingColor = Color.white;
    private Color m_OriginalColor;
    [SerializeField][Min(0)] private int m_FlashCount = 3;
    [SerializeField][Min(0)] private float m_FlashDuration = 0.1f;
    [SerializeField][Min(0)] private float m_InvincibilityTime = 0.5f;
    private bool m_IsInvincible;

    [Header("Damage")]
    [SerializeField] private float m_SpeedMultiplierWhenHit = 0.5f;

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

        if (m_CurrentHealth <= 0)
        {
            Debug.Log(name + " is dead!");
            Destroy(gameObject, 0.5f);
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

    public void TakeDamage(int damage)
    {
        if (!m_IsInvincible)
        {
            Debug.Log(name + "'s Health: " + m_CurrentHealth + " -> " + (m_CurrentHealth - damage));

            m_CurrentHealth -= damage;

            if (TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.linearVelocity = rigidbody.linearVelocity * m_SpeedMultiplierWhenHit;
                rigidbody.angularVelocity = rigidbody.angularVelocity * m_SpeedMultiplierWhenHit;
            }

            StartCoroutine(InvincibleAfterHit());
        }
    }

    private IEnumerator InvincibleAfterHit()
    {
        m_IsInvincible = true;
        StartCoroutine(Flash());
        yield return new WaitForSeconds(m_InvincibilityTime);
        m_IsInvincible = false;
    }

    private IEnumerator Flash()
    {
        Renderer renderer = GetComponent<Renderer>();
        for (int i = 0; i < m_FlashCount; i++)
        {
            renderer.material.color = m_FlashingColor;
            yield return new WaitForSeconds(m_FlashDuration);
            renderer.material.color = m_OriginalColor;
            yield return new WaitForSeconds(m_FlashDuration);
        }
    }
}
