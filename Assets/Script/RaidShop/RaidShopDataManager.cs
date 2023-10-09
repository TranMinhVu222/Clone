using System.Collections.Generic;
using UnityEngine;

public class RaidShopDataManager : DataManager<RaidShopDataManager>
{
    public List<RaidShopItem> raidShopItems;
    void Start()
    {
        string path = Application.dataPath + "/Data/RaidShop.json";
        
        raidShopItems = Initialize<RaidShopItem>(path);
    }
    
    public RaidShopItem GetItemQuantity(string rsId)
    {
        RaidShopItem item = raidShopItems.Find(i => i.raidshop_id == rsId);
        return item;
    }
    
    [System.Serializable]
    public class RaidShopItem
    {
        public int item_id;
        public int price;
        public int quantity;
        public string raidshop_id;
        
        public RaidShopItem() {}
        public RaidShopItem(int itemId, int itemPrice, int itemQuantity, string rsId)
        {
            item_id = itemId;
            price = itemPrice;
            quantity = itemQuantity;
            raidshop_id = rsId;
        }
    }
}
