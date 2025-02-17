using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponManagerScript))]
public class WeaponAmmoBarsScript : MonoBehaviour
{
    #region Variables
    [SerializeField] private Slider m_MagazineAmmoSlider;
    [SerializeField] private Slider m_TotalAmmoSlider;
    private WeaponManagerScript m_WeaponManager;
    #endregion

    #region Main Methods
    void Start()
    {
        m_WeaponManager = GetComponent<WeaponManagerScript>();

        // First we subscribe to the event
        m_WeaponManager.OnAllAmmoUpdate += UpdateAmmoBars;

        // Then we set the max values of the sliders
        // If we don't do this, the sliders of the first weapon will not update correctly
        int[] ammo = m_WeaponManager.GetCurrentWeaponAmmo();
        UpdateAmmoBars(ammo[0], ammo[1]);
    }

    private void OnDestroy()
    {
        m_WeaponManager.OnAllAmmoUpdate -= UpdateAmmoBars;
    }
    #endregion

    #region Update Ammo Bars
    // Method that updates both ammo bars at once
    private void UpdateAmmoBars(int currentMagazineAmmo, int currentTotalAmmo)
    {
        SetMaxAmmoValues();
        UpdateMagazineAmmoBar(currentMagazineAmmo);
        UpdateTotalAmmoBar(currentTotalAmmo);
    }

    // Method that sets the max values of the sliders
    private void SetMaxAmmoValues()
    {
        WeaponScript currentWeaponScript = m_WeaponManager.GetCurrentWeaponScript();

        m_MagazineAmmoSlider.maxValue = currentWeaponScript.m_WeaponData.m_MagazineSize;
        m_TotalAmmoSlider.maxValue = currentWeaponScript.m_WeaponData.m_MaxAmmo;
    }

    // Method that updates the current magazine ammo and activates the slider if needed
    private void UpdateMagazineAmmoBar(int currentMagazineAmmo)
    {
        m_MagazineAmmoSlider.value = currentMagazineAmmo;

        if (currentMagazineAmmo != m_WeaponManager.GetCurrentWeaponScript().m_WeaponData.m_MagazineSize)
        {
            m_MagazineAmmoSlider.gameObject.SetActive(true);
        }
    }

    // Method that updates the current total ammo and activates the slider if needed
    private void UpdateTotalAmmoBar(int currentTotalAmmo)
    {
        m_TotalAmmoSlider.value = currentTotalAmmo;

        if (currentTotalAmmo != m_WeaponManager.GetCurrentWeaponScript().m_WeaponData.m_MaxAmmo)
        {
            m_TotalAmmoSlider.gameObject.SetActive(true);
        }
    }
    #endregion
}
