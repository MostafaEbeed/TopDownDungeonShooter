using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [Header("References")]
    public ShopManager shopManager;
    public GameObject shopItemPrefab;
    public Transform contentParent; // ScrollView content or grid layout

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        foreach (var item in shopManager.GetCurrentShop().availableWeapons)
            CreateShopUIElement(item);

        foreach (var item in shopManager.GetCurrentShop().availableGadgets)
            CreateShopUIElement(item);

        foreach (var item in shopManager.GetCurrentShop().availableAbilities)
            CreateShopUIElement(item);
    }

    private void CreateShopUIElement(BaseItemSO item)
    {
        var obj = Instantiate(shopItemPrefab, contentParent);
        var ui = obj.GetComponent<ShopItemUI>();
        ui.Setup(item, shopManager);
    }
}