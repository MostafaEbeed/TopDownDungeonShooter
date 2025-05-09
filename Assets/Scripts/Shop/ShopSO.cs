using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewShop", menuName = "Shop/Shop Data")]
public class ShopSO : SerializedScriptableObject
{
    [TabGroup("Items", "Weapons")]
    [ListDrawerSettings(Expanded = true)]
    public List<WeaponSO> availableWeapons;

    [TabGroup("Items", "Gadgets")]
    [ListDrawerSettings(Expanded = true)]
    public List<GadgetSO> availableGadgets;

    [TabGroup("Items", "Abilities")]
    [ListDrawerSettings(Expanded = true)]
    public List<AbilitySO> availableAbilities;
}