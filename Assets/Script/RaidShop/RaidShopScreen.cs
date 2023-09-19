using UnityEngine;
using UnityEngine.UI;

public class RaidShopScreen : Screen
{
    [SerializeField] private Text raidTokenText;
    [SerializeField] private GameObject raidShopCellPrefab;

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
                id = itemInfo.id,
                name = itemInfo.name,
                image = itemInfo.image,
                price = raidShopItem.price,
                quantity = raidShopItem.quantity
            };

            GameObject instantiate = Instantiate(raidShopCellPrefab, layout.transform);
            instantiate.GetComponent<RaidShopItemCell>().SetData(product);
        }
    }

    private void ShowRaidToken() => raidTokenText.text = "" + UserInventoryManager.Instance.GetToken();

    public void OnClickInfoBtn()
    {
        int token = UserInventoryManager.Instance.GetToken();
        UserInventoryManager.Instance.SetToken(token + 1000);
        ShowRaidToken();
    }

    public void OnClickBuyItemBtn()
    {
        Debug.Log(gameObject.GetComponent<RaidShopItemCell>().id.ToString());
    }
    
    [System.Serializable]
    public class RaidShopItemInfo
    {
        public int id;
        public string name;
        public Sprite image;
        public int price;
        public int quantity;
    }
}
