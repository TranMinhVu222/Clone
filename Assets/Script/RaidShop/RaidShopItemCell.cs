using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopItemCell : MonoBehaviour
{
    public Text nameItemText;
    public Image imageItem;
    public Text priceItem;
    public Text quantityItem;
    public GameObject chestInfoBt;
    public Text countDownText;
    public int id;

    public void SetData(RaidShopScreen.RaidShopItemInfo data)
    {
        nameItemText.text = data.name;
        imageItem.sprite = data.image;
        priceItem.text = "" + data.price;
        quantityItem.text = data.quantity + " Left";
        if (!data.name.Contains("Chest"))
        {
            chestInfoBt.SetActive(false);
        }

        if (data.image.name.StartsWith("G_"))
        {
            countDownText.gameObject.SetActive(true);
        }

        id = data.id;
    }
    
}