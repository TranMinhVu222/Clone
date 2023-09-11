using System.Collections.Generic;
using UnityEngine;

public class RaidShopDataManager : Manager<RaidShopDataManager>
{
    public List<RaidShopItem> raidShopItems;
    void Start()
    {
        string path = Application.dataPath + "/Data/RaidShop.json";
        
        raidShopItems = Initialize<RaidShopItem>(path);
    }
    
    [System.Serializable]
    public class RaidShopItem
    {
        public int item_id;
        public int price;
        public int quantity;
        public RaidShopItem() {}
        public RaidShopItem(int itemId, int itemPrice, int itemQuantity)
        {
            item_id = itemId;
            price = itemPrice;
            quantity = itemQuantity;
        }
    }
}
