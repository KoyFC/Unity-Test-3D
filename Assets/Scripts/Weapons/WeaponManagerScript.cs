using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
    private InputManager m_Input;
    [SerializeField] private GameObject[] m_WeaponPrefabs;
    private GameObject[] m_Weapons;
    [SerializeField] private int m_CurrentWeaponIndex = 0;

    void Awake()
    {
        m_Weapons = new GameObject[m_WeaponPrefabs.Length];
        for (int i = 0; i < m_WeaponPrefabs.Length; i++)
        {
            m_Weapons[i] = Instantiate(m_WeaponPrefabs[i], transform);
            m_Weapons[i].SetActive(false);
        }
        m_Weapons[m_CurrentWeaponIndex].SetActive(true);
    }

    void Start()
    {
        m_Input = GetComponent<InputManager>();
    }

    void Update()
    {
        if (m_Input.m_MouseWheel != 0)
        {
            int index = m_CurrentWeaponIndex + (int)m_Input.m_MouseWheel;
            if (index < 0)
            {
                index = m_Weapons.Length - 1;
            }
            else if (index >= m_Weapons.Length)
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
        
        Debug.Log("Switched to weapon " + m_CurrentWeaponIndex);
    }
}
