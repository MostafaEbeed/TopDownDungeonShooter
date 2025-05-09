using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIItem : MonoBehaviour
{
    [Header("Refs")]
    public Image itemIcon;
    public TMP_Text levelText;
    public Button addToEquippedButton;
    public Button addToOwnedButton;

    private BaseItemSO item;
    private InventoryUIManager uiManager;

    public void Setup(BaseItemSO newItem, InventoryUIManager manager, bool isEquipped)
    {
        item = newItem;
        uiManager = manager;

        itemIcon.sprite = item.icon;
        levelText.text = $"Lvl: {item.level}";

        // Remove old listeners
        addToEquippedButton.onClick.RemoveAllListeners();
        addToOwnedButton.onClick.RemoveAllListeners();

        // Bind new
        addToEquippedButton.onClick.AddListener(() => uiManager.Equip(item));
        addToOwnedButton.onClick.AddListener(() => uiManager.Unequip(item));

        // If passive ability, hide both buttons
        if (item is AbilitySO ability && ability.abilityType == AbilityType.Passive)
        {
            addToEquippedButton.gameObject.SetActive(false);
            addToOwnedButton.gameObject.SetActive(false);
            return;
        }

        // Otherwise, show and set interactability
        addToEquippedButton.gameObject.SetActive(true);
        addToOwnedButton.gameObject.SetActive(true);

        addToEquippedButton.interactable = !isEquipped;
        addToOwnedButton.interactable    =  isEquipped;
    }
}