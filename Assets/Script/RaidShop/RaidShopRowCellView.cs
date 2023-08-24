using UnityEngine;
using UnityEngine.UI;

public class RaidShopRowCellView: MonoBehaviour
{
    public GameObject container;
    public Text nameItemText;
    public Image imageItem;
    public Text priceItem;
    public Text quantityItem;
    
    public void SetData(RaidShopManager.Product data)
    {
        container.SetActive(data != null);

        if (data != null)
        {
            nameItemText.text = data.name;
            imageItem.sprite = data.image;
            priceItem.text = "" + data.price;
            quantityItem.text = data.quantity + " Left";
        }
    }
}