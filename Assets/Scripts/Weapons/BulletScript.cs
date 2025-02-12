using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
    internal int m_Damage = 0;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<HealthScript>(out var healthScript))
        {
            healthScript.TakeDamage(m_Damage);
        }

        Destroy(gameObject);
    }
}
