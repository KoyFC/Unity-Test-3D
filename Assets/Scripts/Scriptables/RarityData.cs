using UnityEngine;

[CreateAssetMenu(fileName = "RarityData", menuName = "Scriptable Objects/RarityData")]
public class RarityData : ScriptableObject
{
    public enum RarityType
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [System.Serializable]
    public struct Rarity
    {
        public RarityType m_RarityType;
        public Color m_Color;
    }

    [Header("Rarity")]
    public Rarity[] m_Rarities;
}
