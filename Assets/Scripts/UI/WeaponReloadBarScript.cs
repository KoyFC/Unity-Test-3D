using UnityEngine;
using UnityEngine.UI;

public class WeaponReloadBarScript : MonoBehaviour
{
    [SerializeField] private Slider m_Slider;
    private WeaponManagerScript m_WeaponManager;
    private float m_ReloadTime;
    private float m_CurrentTime;

    void Start()
    {
        m_WeaponManager = GetComponent<WeaponManagerScript>();
        m_WeaponManager.OnReload += StartReloadBar;
    }

    void Update()
    {
        if (m_CurrentTime < m_ReloadTime)
        {
            m_CurrentTime += Time.deltaTime;
            m_Slider.value = Mathf.MoveTowards(m_Slider.value, 1f, Time.deltaTime / m_ReloadTime);
        }
        else
        {
            m_Slider.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        m_WeaponManager.OnReload -= StartReloadBar;
    }

    private void StartReloadBar(float time)
    {
        m_ReloadTime = time;
        m_CurrentTime = 0f;
        m_Slider.value = 0f;
        m_Slider.gameObject.SetActive(true);
    }
}
