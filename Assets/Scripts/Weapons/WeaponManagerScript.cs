using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManagerScript : MonoBehaviour
{
    #region Variables
    [Header("Weapons")]
    [SerializeField] private List<GameObject> m_WeaponPrefabs;
    private List<GameObject> m_Weapons;
    private List<WeaponScript> m_WeaponScripts;
    private List<Sprite> m_WeaponSprites;
    [SerializeField] private int m_CurrentWeaponIndex = 0;
    private InputManager m_Input;

    [Header("UI")]
    [SerializeField] private Image m_UIImageReference;

    public event Action<int, int> OnAllAmmoUpdate;
    #endregion

    #region Main Methods
    void Awake()
    {
        m_Weapons = new List<GameObject>();
        m_WeaponScripts = new List<WeaponScript>();
        m_WeaponSprites = new List<Sprite>();

        for (int i = 0; i < m_WeaponPrefabs.Count; i++)
        {
            m_Weapons.Add(Instantiate(m_WeaponPrefabs[i], transform));
            m_Weapons[i].SetActive(false);

            m_WeaponScripts.Add(m_Weapons[i].GetComponent<WeaponScript>());
            m_WeaponSprites.Add(m_WeaponScripts[i].m_WeaponData.m_WeaponSprite);
        }

        m_CurrentWeaponIndex = Mathf.Clamp(m_CurrentWeaponIndex, 0, m_WeaponPrefabs.Count - 1);
        m_Weapons[m_CurrentWeaponIndex].SetActive(true);

        SubscribeEventsCurrentWeapon();
    }

    void Start()
    {
        m_Input = GetComponent<InputManager>();
        m_UIImageReference.sprite = m_WeaponSprites[m_CurrentWeaponIndex];
    }

    void Update()
    {
        bool isReloading = m_WeaponScripts[m_CurrentWeaponIndex].m_IsReloading;

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

    private void OnDestroy()
    {
        UnsubscribeEventsCurrentWeapon();
    }
    #endregion

    #region Event Subscription
    private void SubscribeEventsCurrentWeapon()
    {
        m_WeaponScripts[m_CurrentWeaponIndex].OnAmmoChanged += UpdateAllAmmo;
    }

    private void UnsubscribeEventsCurrentWeapon()
    {
        m_WeaponScripts[m_CurrentWeaponIndex].OnAmmoChanged -= UpdateAllAmmo;
    }

    // Method that invokes its own event to update the ammo UI
    private void UpdateAllAmmo()
    {
        int[] ammo = GetCurrentWeaponAmmo();

        OnAllAmmoUpdate?.Invoke(ammo[0], ammo[1]);
    }
    #endregion

    #region Helper Methods
    void SwitchWeapon(int index)
    {
        // First we unsubscribe from the events of the current weapon
        UnsubscribeEventsCurrentWeapon();
        m_Weapons[m_CurrentWeaponIndex].SetActive(false);

        m_CurrentWeaponIndex = index;
        m_Weapons[m_CurrentWeaponIndex].SetActive(true);
        // Then we subscribe to the events of the new weapon to keep track of its ammo
        SubscribeEventsCurrentWeapon();

        m_UIImageReference.sprite = m_WeaponSprites[m_CurrentWeaponIndex];

        UpdateAllAmmo();
    }

    public void AddAmmoCurrentWeapon(int amount)
    {
        m_Weapons[m_CurrentWeaponIndex].GetComponent<WeaponScript>().AddAmmo(amount);

        UpdateAllAmmo();
    }
    #endregion

    public WeaponScript GetCurrentWeaponScript()
    {
        return m_WeaponScripts[m_CurrentWeaponIndex];
    }

    public int[] GetCurrentWeaponAmmo()
    {
        int[] ammo = new int[2];
        ammo[0] = m_WeaponScripts[m_CurrentWeaponIndex].m_CurrentMagazineAmmo;
        ammo[1] = m_WeaponScripts[m_CurrentWeaponIndex].m_CurrentTotalAmmo;

        return ammo;
    }
}
