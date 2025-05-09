using UnityEngine;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Text displaying current player currency")]
    public TextMeshProUGUI currencyText;

    [Header("Owned Items Panel")]
    [Tooltip("Parent under which owned Weapons, Gadgets, and Active Abilities will be spawned")]
    public Transform ownedItemsParent;

    [Header("Equipped Panels")]
    [Tooltip("Parent under which equipped weapons will be spawned")]
    public Transform equippedWeaponsParent;
    [Tooltip("Parent under which equipped gadgets will be spawned")]
    public Transform equippedGadgetsParent;
    [Tooltip("Parent under which equipped active abilities will be spawned")]
    public Transform equippedActiveAbilitiesParent;
    [Tooltip("Parent under which all owned passive abilities will be spawned")]
    public Transform passiveAbilitiesParent;

    [Header("Item UI Prefab")]
    [Tooltip("Prefab with InventoryUIItem script")]
    public GameObject inventoryUIItemPrefab;

    [Header("Dependencies")]
    [Tooltip("Reference to the player's inventory")]
    public PlayerInventory playerInventory;

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // Update currency
        currencyText.text = $"Currency: {playerInventory.currency}";

        // Clear all panels
        Clear(ownedItemsParent);
        Clear(equippedWeaponsParent);
        Clear(equippedGadgetsParent);
        Clear(equippedActiveAbilitiesParent);
        Clear(passiveAbilitiesParent);

        // 1) Owned Items (Weapons, Gadgets, Active Abilities)
        foreach (var w in playerInventory.ownedWeapons)
            SpawnItem(w, ownedItemsParent, playerInventory.equippedWeapons.Contains(w));

        foreach (var g in playerInventory.ownedGadgets)
            SpawnItem(g, ownedItemsParent, playerInventory.equippedGadgets.Contains(g));

        foreach (var a in playerInventory.ownedAbilities)
            if (a.abilityType == AbilityType.Active)
                SpawnItem(a, ownedItemsParent, playerInventory.equippedActiveAbilities.Contains(a));

        // 2) Equipped Weapons
        foreach (var w in playerInventory.equippedWeapons)
            SpawnItem(w, equippedWeaponsParent, true);

        // 3) Equipped Gadgets
        foreach (var g in playerInventory.equippedGadgets)
            SpawnItem(g, equippedGadgetsParent, true);

        // 4) Equipped Active Abilities
        foreach (var a in playerInventory.equippedActiveAbilities)
            SpawnItem(a, equippedActiveAbilitiesParent, true);

        // 5) All Owned Passive Abilities
        //    (buttons will be hidden by InventoryUIItem)
        foreach (var a in playerInventory.ownedAbilities)
            if (a.abilityType == AbilityType.Passive)
                SpawnItem(a, passiveAbilitiesParent, playerInventory.equippedPassiveAbilities.Contains(a));
    }

    private void Clear(Transform parent)
    {
        foreach (Transform child in parent)
            Destroy(child.gameObject);
    }

    private void SpawnItem(BaseItemSO item, Transform parent, bool isEquipped)
    {
        var go = Instantiate(inventoryUIItemPrefab, parent);
        var ui = go.GetComponent<InventoryUIItem>();
        ui.Setup(item, this, isEquipped);
    }

    public void Equip(BaseItemSO item)
    {
        switch (item)
        {
            case WeaponSO w:  playerInventory.TryEquipWeapon(w);        break;
            case GadgetSO g:  playerInventory.TryEquipGadget(g);        break;
            case AbilitySO a: playerInventory.TryEquipAbility(a);      break;
        }
        RefreshUI();
    }

    public void Unequip(BaseItemSO item)
    {
        switch (item)
        {
            case WeaponSO w:  playerInventory.UnequipWeapon(w);         break;
            case GadgetSO g:  playerInventory.UnequipGadget(g);         break;
            case AbilitySO a: playerInventory.UnequipAbility(a);       break;
        }
        RefreshUI();
    }
}
