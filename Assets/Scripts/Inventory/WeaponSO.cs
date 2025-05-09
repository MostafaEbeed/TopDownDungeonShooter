using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/Weapon")]
public class WeaponSO : BaseItemSO
{
    [BoxGroup("Weapon Data")]
    [Required, LabelText("Weapon Prefab")]
    public GameObject weaponPrefab;

    [BoxGroup("Weapon Stats")]
    [InlineProperty, HideLabel]
    public WeaponStats weaponStats;
}

[System.Serializable]
public class WeaponStats
{
    [MinValue(0.1f)]
    public float fireRate;

    [MinValue(1)]
    public int damage;

    [MinValue(1)]
    public int ammoCapacity;
}
