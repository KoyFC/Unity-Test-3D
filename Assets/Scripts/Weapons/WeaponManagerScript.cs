using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManagerScript : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private List<GameObject> m_WeaponPrefabs;
    private List<GameObject> m_Weapons;
    private List<Sprite> m_WeaponSprites;
    [SerializeField] private int m_CurrentWeaponIndex = 0;
    private InputManager m_Input;

    [Header("UI")]
    [SerializeField] private Image m_UIImageReference;

    void Awake()
    {
        m_Weapons = new List<GameObject>();
        m_WeaponSprites = new List<Sprite>();

        for (int i = 0; i < m_WeaponPrefabs.Count; i++)
        {
            m_Weapons.Add(Instantiate(m_WeaponPrefabs[i], transform));
            m_Weapons[i].SetActive(false);

            m_WeaponSprites.Add(m_Weapons[i].GetComponent<WeaponScript>().m_WeaponData.m_WeaponSprite);
        }

        m_CurrentWeaponIndex = Mathf.Clamp(m_CurrentWeaponIndex, 0, m_WeaponPrefabs.Count - 1);
        m_Weapons[m_CurrentWeaponIndex].SetActive(true);
    }

    void Start()
    {
        m_Input = GetComponent<InputManager>();
        m_UIImageReference.sprite = m_WeaponSprites[m_CurrentWeaponIndex];
    }

    void Update()
    {
        bool isReloading = m_Weapons[m_CurrentWeaponIndex].GetComponent<WeaponScript>().m_IsReloading;

        if (m_Input.m_MouseWheel != 0 && !isReloading)
        {
            int index = m_CurrentWeaponIndex + (int)m_Input.m_MouseWheel;
            if (index < 0)
            {
                index = m_Weapons.Count - 1;
            }
            else if (index >= m_Weapons.Count)
            {
                index = 0;
            }

            SwitchWeapon(index);
        }
    }

    void SwitchWeapon(int index)
    {
        m_Weapons[m_CurrentWeaponIndex].SetActive(false);
        m_CurrentWeaponIndex = index;
        m_Weapons[m_CurrentWeaponIndex].SetActive(true);

        m_UIImageReference.sprite = m_WeaponSprites[m_CurrentWeaponIndex];
    }

    public void AddAmmoCurrentWeapon(int amount)
    {
        m_Weapons[m_CurrentWeaponIndex].GetComponent<WeaponScript>().AddAmmo(amount);
    }

    public WeaponScript GetCurrentWeaponScript()
    {
        return m_Weapons[m_CurrentWeaponIndex].GetComponent<WeaponScript>();
    }
}
