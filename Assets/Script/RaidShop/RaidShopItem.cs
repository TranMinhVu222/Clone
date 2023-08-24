[System.Serializable]
public class RaidShopItem
{
    public int item_id;
    public int price;
    public int quantity;

    public RaidShopItem(int itemId, int itemPrice, int itemQuantity)
    {
        item_id = itemId;
        price = itemPrice;
        quantity = itemQuantity;
    }
}
