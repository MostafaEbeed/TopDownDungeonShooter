using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [Header("UI References")]
    public Image itemIcon;
    public Button buyButton;
    public Button upgradeButton;
    public TMP_Text upgradeCostText;

    private BaseItemSO itemData;
    private ShopManager shopManager;
    private PlayerInventory inventory;

    public void Setup(BaseItemSO item, ShopManager manager)
    {
        itemData = item;
        shopManager = manager;
        inventory = shopManager.GetPlayerInventory();

        itemIcon.sprite = item.icon;
        buyButton.onClick.AddListener(OnBuyClicked);
        upgradeButton.onClick.AddListener(OnUpgradeClicked);

        RefreshUI();
    }

    private void RefreshUI()
    {
        bool owned = inventory.OwnsItem(itemData);

        buyButton.gameObject.SetActive(!owned);
        upgradeButton.gameObject.SetActive(owned);

        if (owned)
            upgradeCostText.text = $"Upgrade: {itemData.upgradeCost}";
    }

    private void OnBuyClicked()
    {
        if (shopManager.TryPurchaseItem(itemData))
        {
            RefreshUI();
        }
    }

    private void OnUpgradeClicked()
    {
        if (shopManager.TryUpgradeItem(itemData))
        {
            RefreshUI();
        }
    }
}