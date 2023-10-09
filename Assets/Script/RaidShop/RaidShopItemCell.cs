using System;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopItemCell : RaidShopItemCellView
{
    private RaidShopScreen.RaidShopItemInfo raidShopItemInfo;
    
    public Text nameItemText;
    public Image imageItem;
    public Text priceItemText;
    public Text quantityItem;
    public GameObject chestInfoBt;
    public Text countDownText;
    public Text inventoryText;
    
    private int id;
    private int price;
    private string rsId;
    
    public event Action<int, int> OnItemBought;
    
    private UserInventoryManager uimInstance = UserInventoryManager.Instance;
    
    public void Initialize(RaidShopScreen screen, Action<int, int> itemBoughtHandler)
    {
        OnItemBought += itemBoughtHandler;
    }
    
    public override void SetData(RaidShopScreen.RaidShopItemInfo data)
    {
        base.SetData(data);

        raidShopItemInfo = data as RaidShopScreen.RaidShopItemInfo;
        
        id = raidShopItemInfo.id;
        price = raidShopItemInfo.price;
        rsId = raidShopItemInfo.rsId;
        
        int quanTemp = raidShopItemInfo.quantity - uimInstance.GetPurchasedItem(rsId);
        nameItemText.text = raidShopItemInfo.name;
        imageItem.sprite = raidShopItemInfo.image;
        priceItemText.text = "" + raidShopItemInfo.price;
        quantityItem.text = quanTemp + " Left";
        if (!raidShopItemInfo.name.Contains("Chest"))
        {
            chestInfoBt.SetActive(false);
        }

        if (raidShopItemInfo.image.name.StartsWith("G_"))
        {
            countDownText.gameObject.SetActive(true);
        }

        inventoryText.text = "" + uimInstance.GetInventoryUser(raidShopItemInfo.id);
    }

    public override void SetItemBought(Action<int, int> action)
    {
        base.SetItemBought(action);
        
        OnItemBought += action;
    }
        
    public void OnClickBuyItemBtn()
    {
        if (uimInstance.GetToken() >= price && uimInstance.GetPurchasedItem(rsId) < RaidShopDataManager.Instance.GetItemQuantity(rsId).quantity)
        {
            int inventoryQuantity = uimInstance.GetInventoryUser(id) + 1;
            uimInstance.SetInventoryUser(new InventoryItem(id,inventoryQuantity));
            Debug.Log(inventoryQuantity);

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