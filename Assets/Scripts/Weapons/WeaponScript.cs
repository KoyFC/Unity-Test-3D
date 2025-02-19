#define AUTO_RELOAD
using UnityEngine;
using System.Collections;
using System;

public class WeaponScript : MonoBehaviour
{
    #region Variables
    [Header("Weapon Data")]
    public WeaponData m_WeaponData; // Public so we can set it in the inspector AND access the sprite in other scripts
    [Tooltip("The offset of the weapon from the player's position.")]
    [SerializeField] private Vector3 m_WeaponOffset;
    private InputManager m_InputManager;

    [Header("Firing")]
    [SerializeField] private Transform m_FirePoint;
    [Tooltip("Should the instantiated bullets inherit the parent's velocity?")]
    [SerializeField] private bool m_AddParentVelocity = false;
    private bool m_CanFire;

    [Header("Ammo")]
    [SerializeField] private bool m_InfiniteAmmo = false;
    public int m_CurrentMagazineAmmo;
    public int m_CurrentTotalAmmo;
    internal bool m_IsReloading;

    public event Action OnAmmoChanged;
    public event Action OnReload;
    #endregion

    #region Main Methods
    private void OnEnable()
    {
        m_CanFire = true;
    }

    void Awake()
    {
        m_InputManager = GetComponentInParent<InputManager>();
        m_CanFire = true;

        m_CurrentMagazineAmmo = m_WeaponData.m_MagazineSize;
        m_CurrentTotalAmmo = m_WeaponData.m_MaxAmmo;
    }

    void Start()
    {
        transform.localPosition = m_WeaponOffset;

        OnAmmoChanged?.Invoke();
    }

    void Update()
    {
        if (m_InputManager.m_FireHeld && m_CanFire && !m_IsReloading)
        {
            StartCoroutine(FireWeapon());
        }

        if (m_InputManager.m_ReloadPressed && !m_IsReloading)
        {
            StartCoroutine(Reload());
        }
    }
    #endregion

    #region Firing
    private IEnumerator FireWeapon()
    {
        m_CanFire = false;

        Fire();

        yield return new WaitForSeconds(m_WeaponData.m_FireRate);

        switch (m_WeaponData.m_FireMode)
        {
            case WeaponData.FireMode.Manual:
                yield return new WaitUntil(() => !m_InputManager.m_FireHeld);
                break;

            case WeaponData.FireMode.ManualBurst:
                for (int i = 0; i < m_WeaponData.m_BurstAmount - 1; i++)
                {
                    yield return new WaitForSeconds(m_WeaponData.m_FireRate);
                    Fire();
                }
                yield return new WaitForSeconds(m_WeaponData.m_CooldownTime);
                yield return new WaitUntil(() => !m_InputManager.m_FireHeld);
                break;

            case WeaponData.FireMode.AutomaticBurst:
                for (int i = 0; i < m_WeaponData.m_BurstAmount - 1; i++)
                {
                    Fire();
                    yield return new WaitForSeconds(m_WeaponData.m_FireRate);
                }
                yield return new WaitForSeconds(m_WeaponData.m_CooldownTime);
                break;
        }
        
        m_CanFire = true;
    }

    private void Fire()
    {
        if (!m_InfiniteAmmo)
        {
            if (m_CurrentMagazineAmmo <= 0)
            {
                #if AUTO_RELOAD
                StartCoroutine(Reload());
                #endif

                return;
            }
        
            m_CurrentMagazineAmmo--;
            OnAmmoChanged?.Invoke();
        }

        GameObject bullet = Instantiate(m_WeaponData.m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);

        Vector3 parentVelocity = Vector3.zero;
        
        if (m_AddParentVelocity && transform.parent.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            parentVelocity = rigidbody.linearVelocity;
        }

        bullet.GetComponent<Rigidbody>().linearVelocity = parentVelocity + m_FirePoint.forward * m_WeaponData.m_BulletVelocity;
        bullet.GetComponent<BulletScript>().m_Damage = m_WeaponData.m_BulletDamage;
    }

    private IEnumerator Reload()
    {
        if (m_CurrentTotalAmmo <= 0 || m_CurrentMagazineAmmo >= m_WeaponData.m_MagazineSize) yield break;

        m_IsReloading = true;
        OnReload?.Invoke();

        yield return new WaitForSeconds(m_WeaponData.m_ReloadTime);

        m_IsReloading = false;

        // If the player has less ammo than the magazine size, just fill the magazine with the remaining ammo. 
        // Otherwise, fill the magazine from the total ammo.
        if (m_WeaponData.m_MagazineSize > m_CurrentTotalAmmo)
        {
            m_CurrentMagazineAmmo = m_CurrentTotalAmmo;
            m_CurrentTotalAmmo = 0;
        }
        else
        {
            m_CurrentTotalAmmo -= m_WeaponData.m_MagazineSize - m_CurrentMagazineAmmo;
            m_CurrentMagazineAmmo = m_WeaponData.m_MagazineSize;
        }

        OnAmmoChanged?.Invoke();
    }
    #endregion

    internal void AddAmmo(int amount)
    {
        m_CurrentTotalAmmo += amount;

        if (m_CurrentTotalAmmo > m_WeaponData.m_MaxAmmo)
        {
            m_CurrentTotalAmmo = m_WeaponData.m_MaxAmmo;
        }
    }
}
