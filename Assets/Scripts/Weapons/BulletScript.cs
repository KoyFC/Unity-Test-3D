using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
    internal int m_Damage = 0;

    private void Start()
    {
        Destroy(gameObject, 3f);
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

        Destroy(gameObject);
    }
}
