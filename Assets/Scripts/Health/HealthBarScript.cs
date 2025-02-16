using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthScript))]
public class HealthBarScript : MonoBehaviour
{
    private Slider m_Slider;
    private HealthScript m_HealthScript;


    void Awake()
    {
        m_HealthScript = GetComponent<HealthScript>();
        m_Slider = GetComponentInChildren<Slider>();

        m_Slider.maxValue = m_HealthScript.m_MaxHealth;

        m_HealthScript.OnHealthChanged += UpdateHealthBar;
    }

    void OnDestroy()
    {
        m_HealthScript.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth)
    {
        m_Slider.value = currentHealth;

        if (currentHealth != m_HealthScript.m_MaxHealth)
        {
            m_Slider.gameObject.SetActive(true);
        }
        else
        {
            m_Slider.gameObject.SetActive(false);
        }
    }
}