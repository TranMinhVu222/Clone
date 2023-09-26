using UnityEngine;
using UnityEngine.UI;

public class RaidShopItemCell : MonoBehaviour
{
    public Text nameItemText;
    public Image imageItem;
    public Text priceItemText;
    public Text quantityItem;
    public GameObject chestInfoBt;
    public Text countDownText;
    public Text inventoryText;
    private GameObject rsScreenPrefab;
    private UserInventoryManager uimInstance = UserInventoryManager.Instance;

    public int id;
    private int price;
    private string rsId;

    public void SetData(RaidShopScreen.RaidShopItemInfo data)
    {
        id = data.id;
        price = data.price;
        rsId = data.rsId;
        
        int quanTemp = data.quantity - uimInstance.GetPurchasedItem(rsId);
        nameItemText.text = data.name;
        imageItem.sprite = data.image;
        priceItemText.text = "" + data.price;
        quantityItem.text = quanTemp + " Left";
        if (!data.name.Contains("Chest"))
        {
            chestInfoBt.SetActive(false);
        }

        if (data.image.name.StartsWith("G_"))
        {
            countDownText.gameObject.SetActive(true);
        }

        inventoryText.text = "" + uimInstance.GetInventoryUser(data.id);
    }
    
    public void OnClickBuyItemBtn()
    {
        if (uimInstance.GetToken() >= price && uimInstance.GetPurchasedItem(rsId) < RaidShopDataManager.Instance.GetItemQuantity(rsId).quantity)
        {
            int inventoryQuantity = uimInstance.GetInventoryUser(id) + 1;
            uimInstance.SetInventoryUser(new InventoryItem(id,inventoryQuantity));

            int purchasedToken = uimInstance.GetToken() - price;
            uimInstance.SetToken(purchasedToken);
            
            int purchasedQuantity = uimInstance.GetPurchasedItem(rsId) + 1;
            uimInstance.SetPurchasedItem(rsId,purchasedQuantity);
            quantityItem.text = RaidShopDataManager.Instance.GetItemQuantity(rsId).quantity -  uimInstance.GetPurchasedItem(rsId) + " Left";
            
            RaidShopScreen raidShopScreen = FindObjectOfType<RaidShopScreen>();
            raidShopScreen.ShowRaidToken();
            raidShopScreen.ChangeColor();
            raidShopScreen.InventoryUser(id,inventoryQuantity);
        }
    }
    
    public void ChangeColorPriceText()
    {
        if (UserInventoryManager.Instance.GetToken() < price)
        {
            priceItemText.color = new Color(1,  0.259434f, 0.3192778f,1);
        }
        else
        {
            priceItemText.color = new Color(1,1,1,1f);
        }
    }
}