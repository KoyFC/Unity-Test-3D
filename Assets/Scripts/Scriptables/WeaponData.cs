using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public enum FireMode
    {
        Manual,
        Automatic,
        ManualBurst,
        AutomaticBurst
    }

    [Header("Weapon")]
    public string m_UniqueWeaponID = "generic_ID";
    public string m_WeaponName = "Unnamed Weapon";
    [Tooltip("The sprite that will be displayed in the UI for this weapon.")]
    public Sprite m_WeaponSprite;
    [Tooltip("The amount of bullets the weapon can shoot before needing to reload.")]
    [Min(0)] public int m_MagazineSize = 30;
    [Tooltip("The maximum amount of ammo the player can hold for this weapon.")]
    [Min(0)] public int m_MaxAmmo = 120;
    [Min(0)] public float m_ReloadTime = 1.5f;

    [Header("Firing")]
    public FireMode m_FireMode = FireMode.Automatic;
    [Tooltip("The time between each bullet being shot, or the cooldown in manual mode.")]
    [Min(0)]public float m_FireRate = 0.1f;

    [Header("Burst Only Settings")]
    [Tooltip("The number of shots the weapon will fire when in a burst mode.")]
    [Min(0)] public int m_BurstAmount = 3;
    [Tooltip("The cooldown time between each burst when in a burst mode.")]
    [Min(0)] public float m_CooldownTime = 0.5f;

    [Header("Bullet")]
    public GameObject m_BulletPrefab = null;
    public int m_BulletDamage = 10;
    public float m_BulletVelocity = 15f;
    public GameObject m_ImpactParticles;
}
