using UnityEngine;
using UnityEngine.UI;

public class WeaponManagerScript : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private GameObject[] m_WeaponPrefabs;
    private GameObject[] m_Weapons;
    private Sprite[] m_WeaponSprites;
    [SerializeField] private int m_CurrentWeaponIndex = 0;
    private InputManager m_Input;

    [Header("UI")]
    [SerializeField] private Image m_UIImageReference;

    void Awake()
    {
        m_Weapons = new GameObject[m_WeaponPrefabs.Length];
        m_WeaponSprites = new Sprite[m_WeaponPrefabs.Length];

        for (int i = 0; i < m_WeaponPrefabs.Length; i++)
        {
            m_Weapons[i] = Instantiate(m_WeaponPrefabs[i], transform);
            m_Weapons[i].SetActive(false);

            m_WeaponSprites[i] = m_Weapons[i].GetComponent<WeaponScript>().m_WeaponData.m_WeaponSprite;
        }
        m_Weapons[m_CurrentWeaponIndex].SetActive(true);
    }

    void Start()
    {
        m_Input = GetComponent<InputManager>();
        m_UIImageReference.sprite = m_WeaponSprites[m_CurrentWeaponIndex];
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

        m_UIImageReference.sprite = m_WeaponSprites[m_CurrentWeaponIndex];
        
        Debug.Log("Switched to weapon " + m_CurrentWeaponIndex);
    }
}
