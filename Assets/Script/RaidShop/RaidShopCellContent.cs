using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopCellContent : MonoBehaviour
{
    public Text nameItemText;
    public Image imageItem;
    public Text priceItem;
    public Text quantityItem;

    public void SetData(RaidShopScreen.Product data)
    {
        nameItemText.text = data.name;
        imageItem.sprite = data.image;
        priceItem.text = "" + data.price;
        quantityItem.text = data.quantity + " Left";
    }
}
