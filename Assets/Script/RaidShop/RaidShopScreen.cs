using UnityEngine;
using UnityEngine.UI;

public class RaidShopScreen : Screen
{
    public GameObject raidShopCellPrefab;
    public GameObject budgetInfo;
    
    [SerializeField] private GridLayoutGroup layout;


    private void Start()
    {
        ShowRaidShopItemInfo();
        
        ShowRaidToken();
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

    void ShowRaidToken()
    {
        budgetInfo.GetComponent<ShowBudgetInfo>().CurrentRaidToken(RaidUserManager.Instance.SetData());
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
