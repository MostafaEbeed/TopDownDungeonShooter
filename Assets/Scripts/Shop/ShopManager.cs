using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopSO currentShop;
    [SerializeField] private PlayerInventory playerInventory;

    public bool TryPurchaseItem(BaseItemSO item)
    {
        if (item == null || playerInventory == null) return false;
        if (playerInventory.currency < item.cost) return false;

        bool added = false;

        switch (item)
        {
            case WeaponSO weapon:
                added = playerInventory.AddWeapon(weapon);
                break;
            case GadgetSO gadget:
                added = playerInventory.AddGadget(gadget);
                break;
            case AbilitySO ability:
                added = playerInventory.AddAbility(ability);
                break;
        }

        if (added)
        {
            playerInventory.SpendCurrency(item.cost);
            Debug.Log($"{item.itemName} purchased!");
            return true;
        }

        return false;
    }

    public ShopSO GetCurrentShop() => currentShop;
    
    public PlayerInventory GetPlayerInventory() => playerInventory;
    
    public bool TryUpgradeItem(BaseItemSO item)
    {
        if (!playerInventory.OwnsItem(item) || playerInventory.currency < item.upgradeCost)
            return false;

        item.Upgrade();
        playerInventory.SpendCurrency(item.upgradeCost);
        Debug.Log($"{item.name} upgraded!");

        return true;
    }
}