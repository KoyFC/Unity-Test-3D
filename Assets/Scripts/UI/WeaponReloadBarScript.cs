using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponReloadBarScript : MonoBehaviour
{
    [SerializeField] private Slider m_Slider = null;
    private WeaponManagerScript m_WeaponManager = null;
    private float m_ReloadTime = 1f;
    private float m_CurrentTime = 0f;

    void Start()
    {
        m_WeaponManager = GetComponent<WeaponManagerScript>();
        m_WeaponManager.OnReload += StartReloadBar;
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

        StartCoroutine(UpdateReloadBar());
    }

    private IEnumerator UpdateReloadBar()
    {
        while (m_CurrentTime < m_ReloadTime)
        {
            m_CurrentTime += Time.deltaTime;
            m_Slider.value = Mathf.MoveTowards(m_Slider.value, 1f, Time.deltaTime / m_ReloadTime);
            yield return null;
        }
        
        m_Slider.gameObject.SetActive(false);
    }
}
