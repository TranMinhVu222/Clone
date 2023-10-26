using System;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopItemCell : MonoBehaviour
{
    private RaidShopScreen.RaidShopItemInfo raidShopItemInfo;
    
    public Text nameItemText;
    public Image imageItem;
    public Text priceItemText;
    public Text quantityItem;
    public GameObject chestInfoBt;
    public Text countDownText;
    public Text inventoryText;
    public Text buyText;

    private int id;
    private int price;
    private string rsId;
    
    public Action<int, int> OnItemBought;
    
    private UserInventoryManager uimInstance = UserInventoryManager.Instance;
    
    public void SetData(RaidShopScreen.RaidShopItemInfo data)
    {
        raidShopItemInfo = data as RaidShopScreen.RaidShopItemInfo;
        
        id = raidShopItemInfo.id;
        price = raidShopItemInfo.price;
        rsId = raidShopItemInfo.rsId;
        
        int quanTemp = raidShopItemInfo.quantity - uimInstance.GetPurchasedItem(rsId);
        nameItemText.text = raidShopItemInfo.name;
        imageItem.sprite = raidShopItemInfo.image;
        priceItemText.text = "" + raidShopItemInfo.price;
        quantityItem.text = quanTemp + " " + LocalizationManager.Instance.GetLocalizedValue("left");
        if (id < 2)
        {
            chestInfoBt.SetActive(false);
        }

        if (raidShopItemInfo.image.name.StartsWith("G_"))
        {
            countDownText.gameObject.SetActive(true);
            countDownText.text = "15 " +  LocalizationManager.Instance.GetLocalizedValue("minutes");
        }
        buyText.text = LocalizationManager.Instance.GetLocalizedValue("buy");
        inventoryText.text = "" + uimInstance.GetInventoryUser(raidShopItemInfo.id).quantity;
    }
    
    public void OnClickBuyItemBtn()
    {
        if (uimInstance.GetToken() >= price && uimInstance.GetPurchasedItem(rsId) < RaidShopDataManager.Instance.GetItemQuantity(rsId).quantity)
        {
            int inventoryQuantity = uimInstance.GetInventoryUser(id).quantity + 1;
            uimInstance.SetInventoryUser(id.ToString(),new InventoryItemsData(id.ToString(),inventoryQuantity)); ;
            int purchasedToken = uimInstance.GetToken() - price;
            uimInstance.SetToken(purchasedToken);
            
            int purchasedQuantity = uimInstance.GetPurchasedItem(rsId) + 1;
            uimInstance.SetPurchasedItem(rsId,purchasedQuantity);
            quantityItem.text = RaidShopDataManager.Instance.GetItemQuantity(rsId).quantity -  uimInstance.GetPurchasedItem(rsId) + " Left";
            
            OnItemBought?.Invoke(id, inventoryQuantity);
        }
    }
    
    public int GetId() => id;
    public int GetPrice() => price;
}