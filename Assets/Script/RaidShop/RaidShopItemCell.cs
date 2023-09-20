using System.Collections;
using System.Collections.Generic;
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
    public GameObject rsScreenPrefab;

    private int id, price;

    public void SetData(RaidShopScreen.RaidShopItemInfo data)
    {
        nameItemText.text = data.name;
        imageItem.sprite = data.image;
        priceItemText.text = "" + data.price;
        quantityItem.text = data.quantity + " Left";
        if (!data.name.Contains("Chest"))
        {
            chestInfoBt.SetActive(false);
        }

        if (data.image.name.StartsWith("G_"))
        {
            countDownText.gameObject.SetActive(true);
        }

        inventoryText.text = "" + UserInventoryManager.Instance.GetInventoryUser(data.id); 

        id = data.id;
        price = data.price;
    }
    
    public void OnClickBuyItemBtn()
    {
        if (UserInventoryManager.Instance.GetToken() >= price)
        {
            int quantity = UserInventoryManager.Instance.GetInventoryUser(id) + 1;
            UserInventoryManager.Instance.SetInventoryUser(id,quantity);

            int purchasedToken = UserInventoryManager.Instance.GetToken() - price;
            UserInventoryManager.Instance.SetToken(purchasedToken);
            
            inventoryText.text = "" + UserInventoryManager.Instance.GetInventoryUser(id);
        }
        
        rsScreenPrefab.GetComponent<RaidShopScreen>().ShowRaidToken();
        rsScreenPrefab.GetComponent<RaidShopScreen>().ChangeColor();
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