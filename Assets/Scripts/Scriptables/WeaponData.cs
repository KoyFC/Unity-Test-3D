using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public enum FireMode
    {
        Manual,
        Automatic,
        Burst
    }

    [Header("Weapon")]
    public string m_UniqueWeaponID;
    public string m_WeaponName;
    public Sprite m_WeaponSprite;
    [Min(0)] public int m_MagazineSize = 30;
    [Min(0)] public int m_MaxAmmo = 120;
    [Min(0)] public float m_ReloadTime = 1.5f;

    [Header("Firing")]
    public FireMode m_FireMode;
    [Min(0)] public int m_BurstAmount = 3;
    [Min(0)] public float m_CooldownTime = 0.5f;
    public float m_FireRate = 0.1f;

    [Header("Bullet")]
    public GameObject m_BulletPrefab;
    public int m_BulletDamage = 10;
    public float m_BulletVelocity = 15f;
    public GameObject m_ImpactParticles;
}
