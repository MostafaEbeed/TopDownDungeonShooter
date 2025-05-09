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

    public bool OwnsItem(BaseItemSO item)
    {
        return ownedWeapons.Contains(item as WeaponSO)
               || ownedGadgets.Contains(item as GadgetSO)
               || ownedAbilities.Contains(item as AbilitySO);
    }
    
    public void EquipItem(BaseItemSO item)
    {
        if (!OwnsItem(item)) return;

        switch (item)
        {
            case WeaponSO weapon when !equippedWeapons.Contains(weapon):
                TryEquipWeapon(weapon);
                break;
            case GadgetSO gadget when !equippedGadgets.Contains(gadget):
                TryEquipGadget(gadget);
                break;
            case AbilitySO ability when !equippedActiveAbilities.Contains(ability) && !equippedPassiveAbilities.Contains(ability):
                TryEquipAbility(ability);
                break;
        }
    }

    public void UnequipItem(BaseItemSO item)
    {
        switch (item)
        {
            case WeaponSO weapon:
                UnequipWeapon(weapon);
                break;
            case GadgetSO gadget:
                UnequipGadget(gadget);
                break;
            case AbilitySO ability:
                UnequipAbility(ability);
                break;
        }
    }
    
    public bool IsEquipped(BaseItemSO item)
    {
        return (item is WeaponSO   w  && equippedWeapons.Contains(w))
               || (item is GadgetSO   g  && equippedGadgets.Contains(g))
               || (item is AbilitySO a  
                   && (a.abilityType == AbilityType.Active 
                       ? equippedActiveAbilities.Contains(a) 
                       : equippedPassiveAbilities.Contains(a))
               );
    }
    
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
        RemoveWeapon(weapon);
        return true;
    }

    public bool UnequipWeapon(WeaponSO weapon)
    {
        AddWeapon(weapon);
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
        RemoveGadget(gadget);
        return true;
    }

    public bool UnequipGadget(GadgetSO gadget)
    {
        AddGadget(gadget);
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
        ownedAbilities.Remove(ability);

        return true;
    }

    public void UnequipAbility(AbilitySO ability)
    {
        if (ability.abilityType == AbilityType.Active)
        {
            ownedAbilities.Add(ability);
            equippedActiveAbilities.Remove(ability);
        }
    }
    #endregion
}
