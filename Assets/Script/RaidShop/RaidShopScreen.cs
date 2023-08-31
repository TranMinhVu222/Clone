using UnityEngine;
using UnityEngine.UI;

public class RaidShopScreen : Screen
{
    public GameObject raidShopCellPrefab;
    [SerializeField] private GridLayoutGroup layout;


    private void Start()
    {
        ShowRaidShopItemInfo();
    }
    
    void  ShowRaidShopItemInfo()
    {
        foreach (var raidShopItem in RaidShopDataManager.Instance.raidShopItems)
        {
            ItemDataManager.Item itemInfo = ItemDataManager.Instance.GetItem(raidShopItem.item_id);
            
            var product = new RaidShopItemInfo
            {
                name = itemInfo.name,
                image = itemInfo.image,
                price = raidShopItem.price,
                quantity = raidShopItem.quantity
            };

            GameObject instantiate = Instantiate(raidShopCellPrefab, layout.transform);
            instantiate.GetComponent<RaidShopItemCell>().SetData(product);
        }
    }
    
    
    [System.Serializable]
    public class RaidShopItemInfo
    {
        public string name;
        public Sprite image;
        public int price;
        public int quantity;
    }
}
