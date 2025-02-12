using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    private InputManager m_InputManager;

    [Header("Firing")]
    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private float m_FireRate = 0.1f;
    private bool m_CanFire;

    [Header("Bullet")]
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private int m_BulletDamage = 10;
    [SerializeField] private float m_BulletVelocity = 15f;

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

        GameObject bullet = Instantiate(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);

        bullet.GetComponent<Rigidbody>().linearVelocity = m_FirePoint.forward * m_BulletVelocity;
        bullet.GetComponent<BulletScript>().m_Damage = m_BulletDamage;

        yield return new WaitForSeconds(m_FireRate);

        m_CanFire = true;
    }
}
