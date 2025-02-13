using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private WeaponData m_WeaponData;
    protected InputManager m_InputManager;

    [Header("Firing")]
    [SerializeField] private Transform m_FirePoint;
    protected bool m_CanFire;

    void Start()
    {
        m_InputManager = GetComponentInParent<InputManager>();
        m_CanFire = true;
    }

    void Update()
    {
        if (m_InputManager.m_FireHeld && m_CanFire)
        {
            StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        m_CanFire = false;

        GameObject bullet = Instantiate(m_WeaponData.m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);

        bullet.GetComponent<Rigidbody>().linearVelocity = m_FirePoint.forward * m_WeaponData.m_BulletVelocity;
        bullet.GetComponent<BulletScript>().m_Damage = m_WeaponData.m_BulletDamage;

        yield return new WaitForSeconds(m_WeaponData.m_FireRate);

        m_CanFire = true;
    }
}
