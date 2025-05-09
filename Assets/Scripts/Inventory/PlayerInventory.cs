using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int currency;

    public List<WeaponSO> ownedWeapons = new();
    public List<GadgetSO> ownedGadgets = new();
    public List<AbilitySO> ownedAbilities = new();

    public List<WeaponSO> equippedWeapons = new();     
    public List<GadgetSO> equippedGadgets = new();     
    public List<AbilitySO> equippedActiveAbilities = new(); 
    public List<AbilitySO> equippedPassiveAbilities = new(); 

    public int maxActiveAbilities = 2;
    public int maxWeapons = 2;
    public int maxGadgets = 2;

    #region Currency
    public void AddCurrency(int amount) => currency += amount;

    public bool SpendCurrency(int amount)
    {
        if (currency < amount) return false;
        currency -= amount;
        return true;
    }
    #endregion

    #region Weapon Management
    public bool AddWeapon(WeaponSO weapon)
    {
        if (ownedWeapons.Contains(weapon)) return false;
        ownedWeapons.Add(weapon);
        return true;
    }

    public bool RemoveWeapon(WeaponSO weapon)
    {
        return ownedWeapons.Remove(weapon);
    }

    public bool TryEquipWeapon(WeaponSO weapon)
    {
        if (equippedWeapons.Count >= maxWeapons || !ownedWeapons.Contains(weapon)) return false;
        equippedWeapons.Add(weapon);
        return true;
    }

    public bool UnequipWeapon(WeaponSO weapon)
    {
        return equippedWeapons.Remove(weapon);
    }
    #endregion

    #region Gadget Management
    public bool AddGadget(GadgetSO gadget)
    {
        if (ownedGadgets.Contains(gadget)) return false;
        ownedGadgets.Add(gadget);
        return true;
    }

    public bool RemoveGadget(GadgetSO gadget)
    {
        return ownedGadgets.Remove(gadget);
    }

    public bool TryEquipGadget(GadgetSO gadget)
    {
        if (equippedGadgets.Count >= maxGadgets || !ownedGadgets.Contains(gadget)) return false;
        equippedGadgets.Add(gadget);
        return true;
    }

    public bool UnequipGadget(GadgetSO gadget)
    {
        return equippedGadgets.Remove(gadget);
    }
    #endregion

    #region Ability Management
    public bool AddAbility(AbilitySO ability)
    {
        if (ownedAbilities.Contains(ability)) return false;
        ownedAbilities.Add(ability);
        return true;
    }

    public bool RemoveAbility(AbilitySO ability)
    {
        return ownedAbilities.Remove(ability);
    }

    public bool TryEquipAbility(AbilitySO ability)
    {
        if (!ownedAbilities.Contains(ability)) return false;

        if (ability.abilityType == AbilityType.Active)
        {
            if (equippedActiveAbilities.Count >= maxActiveAbilities) return false;
            equippedActiveAbilities.Add(ability);
        }
        else
        {
            equippedPassiveAbilities.Add(ability);
        }

        return true;
    }

    public bool UnequipAbility(AbilitySO ability)
    {
        if (ability.abilityType == AbilityType.Active)
            return equippedActiveAbilities.Remove(ability);
        else
            return equippedPassiveAbilities.Remove(ability);
    }
    #endregion
}
