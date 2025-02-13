using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon")]
    public string m_WeaponName;
    public Sprite m_WeaponIcon;
    public int m_MagazineSize = 30;
    public int m_MaxAmmo = 120;
    public float m_ReloadTime = 1.5f;

    [Header("Firing")]
    public float m_FireRate = 0.1f;

    [Header("Bullet")]
    public GameObject m_BulletPrefab;
    public int m_BulletDamage = 10;
    public float m_BulletVelocity = 15f;
    public GameObject m_ImpactParticles;
}
