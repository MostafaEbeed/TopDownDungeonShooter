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

    public void Setup(BaseItemSO item, ShopManager manager)
    {
        itemData = item;
        shopManager = manager;

        itemIcon.sprite = item.icon;
        buyButton.onClick.AddListener(OnBuyClicked);
        upgradeButton.onClick.AddListener(OnUpgradeClicked);

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (itemData.isOwned)
        {
            buyButton.gameObject.SetActive(false);
            upgradeButton.gameObject.SetActive(true);
            upgradeCostText.text = $"Upgrade: {itemData.upgradeCost}💰";
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            upgradeButton.gameObject.SetActive(false);
        }
    }

    private void OnBuyClicked()
    {
        if (shopManager.TryPurchaseItem(itemData))
        {
            itemData.isOwned = true;
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