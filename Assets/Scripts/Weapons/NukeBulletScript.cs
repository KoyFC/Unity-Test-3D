using UnityEngine;

public class NukeBulletScript : BulletScript
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<HealthScript>(out var healthScript))
        {
            healthScript.TakeDamage(m_Damage);
        }

        if (other.gameObject.layer == gameObject.layer)
        {
            Destroy(other.gameObject);
        }
        
        if (m_DestroyOnHit)
        {
            Destroy(gameObject);
        }
    }
}
