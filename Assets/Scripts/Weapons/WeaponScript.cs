using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] protected WeaponData m_WeaponData;
    protected InputManager m_InputManager;

    [Header("Firing")]
    [SerializeField] protected Transform m_FirePoint;
    protected bool m_CanFire;

    private void OnEnable()
    {
        m_CanFire = true;
        
    }

    void Awake()
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

    protected virtual IEnumerator Fire()
    {
        m_CanFire = false;

        GameObject bullet = Instantiate(m_WeaponData.m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);

        Vector3 parentVelocity = Vector3.zero;

        if (transform.parent.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            parentVelocity = rigidbody.linearVelocity;
        }

        bullet.GetComponent<Rigidbody>().linearVelocity = parentVelocity + m_FirePoint.forward * m_WeaponData.m_BulletVelocity;
        bullet.GetComponent<BulletScript>().m_Damage = m_WeaponData.m_BulletDamage;

        yield return new WaitForSeconds(m_WeaponData.m_FireRate);

        m_CanFire = true;
    }
}
