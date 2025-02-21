using UnityEngine;

public class AmmoPickupScript : MonoBehaviour
{
    [SerializeField] private string m_AmmoID = "generic_ID";
    [SerializeField] private int m_AmmoAmount = 20;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            if (other.TryGetComponent<WeaponManagerScript>(out var weaponManager) && weaponManager.GetCurrentWeaponScript().m_WeaponData.m_UniqueWeaponID == m_AmmoID)
            {
                weaponManager.AddAmmoCurrentWeapon(m_AmmoAmount);
                Destroy(gameObject);
            }
        }
    }
}
