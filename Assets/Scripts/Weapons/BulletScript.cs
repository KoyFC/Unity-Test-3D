using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
    public Rigidbody m_Rigidbody = null;

    [SerializeField] private string m_AmmoUniqueID = "generic_ID";
    [SerializeField] private float m_LifeTime = 3f;
    [SerializeField] protected bool m_DestroyOnHit = true;
    internal int m_Damage = 0;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        StartCoroutine(DestroyBullet());
    }

    private void OnDisable()
    {
        m_Rigidbody.linearVelocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<HealthScript>(out var healthScript))
        {
            healthScript.TakeDamage(m_Damage);
        }

        if (collision.gameObject.layer == gameObject.layer)
        {
            return;
        }

        if (m_DestroyOnHit)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(m_LifeTime);
        BulletPoolManagerScript.Instance.ReturnBullet(gameObject);
    }
}
