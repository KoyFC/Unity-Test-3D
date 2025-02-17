using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponManagerScript))]
public class WeaponReloadBarScript : MonoBehaviour
{
    #region Variables
    [SerializeField] private Slider m_MagazineAmmoSlider;
    [SerializeField] private Slider m_TotalAmmoSlider;
    private WeaponManagerScript m_WeaponManagerScript;
    #endregion

    #region Main Methods
    void Start()
    {
        m_WeaponManagerScript = GetComponent<WeaponManagerScript>();

        // First we subscribe to the events
        SubscribeAllEvents();

        // Then we set the max values of the sliders
        // If we don't do this, the sliders of the first weapon will not update correctly
        int[] ammo = m_WeaponManagerScript.GetCurrentWeaponAmmo();
        UpdateAmmoBars(ammo[0], ammo[1]);
    }

    private void OnDestroy()
    {
        UnsubscribeAllEvents();
    }
    #endregion

    #region Event Subscription
    private void SubscribeAllEvents()
    {
        m_WeaponManagerScript.OnAllAmmoUpdate += UpdateAmmoBars;

        // Creating a variable to reduce the amount of Get calls
        WeaponScript currentWeaponScript = m_WeaponManagerScript.GetCurrentWeaponScript();

        currentWeaponScript.OnMagazineAmmoChanged += UpdateMagazineAmmoBar;
        currentWeaponScript.OnTotalAmmoChanged += UpdateTotalAmmoBar;
    }

    private void UnsubscribeAllEvents()
    {
        m_WeaponManagerScript.OnAllAmmoUpdate -= UpdateAmmoBars;

        // Creating a variable to reduce the amount of Get calls
        WeaponScript currentWeaponScript = m_WeaponManagerScript.GetCurrentWeaponScript();

        currentWeaponScript.OnMagazineAmmoChanged -= UpdateMagazineAmmoBar;
        currentWeaponScript.OnTotalAmmoChanged -= UpdateTotalAmmoBar;
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
        WeaponScript currentWeaponScript = m_WeaponManagerScript.GetCurrentWeaponScript();

        m_MagazineAmmoSlider.maxValue = currentWeaponScript.m_WeaponData.m_MagazineSize;
        m_TotalAmmoSlider.maxValue = currentWeaponScript.m_WeaponData.m_MaxAmmo;
    }

    // Method that updates the current magazine ammo
    private void UpdateMagazineAmmoBar(int currentMagazineAmmo)
    {
        m_MagazineAmmoSlider.value = currentMagazineAmmo;

        if (currentMagazineAmmo != m_WeaponManagerScript.GetCurrentWeaponScript().m_WeaponData.m_MagazineSize)
        {
            m_MagazineAmmoSlider.gameObject.SetActive(true);
        }
    }

    // Method that updates the current total ammo
    private void UpdateTotalAmmoBar(int currentTotalAmmo)
    {
        m_TotalAmmoSlider.value = currentTotalAmmo;

        if (currentTotalAmmo != m_WeaponManagerScript.GetCurrentWeaponScript().m_WeaponData.m_MaxAmmo)
        {
            m_TotalAmmoSlider.gameObject.SetActive(true);
        }
    }
    #endregion
}
