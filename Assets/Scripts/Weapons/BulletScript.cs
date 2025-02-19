using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
    [SerializeField] private float m_LifeTime = 3f;
    [SerializeField] protected bool m_DestroyOnHit = true;
    internal int m_Damage = 0;

    private void Start()
    {
        Destroy(gameObject, m_LifeTime);
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
}
