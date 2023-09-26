using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopScreen : Screen
{
    [SerializeField] private Text raidTokenText;
    [SerializeField] private GameObject raidShopCellPrefab;

    [SerializeField] private GridLayoutGroup layout;

    private List<GameObject> rsItemCellList = new List<GameObject>();
    private void Start()
    {
        ShowRaidShopItemInfo();
        
        ShowRaidToken();
        
        ChangeColor();
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
                quantity = raidShopItem.quantity,
                rsId = raidShopItem.raidshop_id
            };
            
            GameObject instantiate = Instantiate(raidShopCellPrefab, layout.transform);
            instantiate.GetComponent<RaidShopItemCell>().SetData(product);
            rsItemCellList.Add(instantiate);
        }
    }
    
    public void OnClickInfoBtn()
    {
        int token = UserInventoryManager.Instance.GetToken();
        UserInventoryManager.Instance.SetToken(token + 1000);
        ShowRaidToken();
        ChangeColor();
    }
    
    public void ShowRaidToken() => raidTokenText.text = "" + UserInventoryManager.Instance.GetToken();
    
    public void ChangeColor()
    {
        foreach (var item in rsItemCellList)
        {
            item.GetComponent<RaidShopItemCell>().ChangeColorPriceText();
        }
    }

    public void InventoryUser(int itemId, int quantity)
    {
        foreach (var item in rsItemCellList)
        {
            RaidShopItemCell raidShopItemCell = item.GetComponent<RaidShopItemCell>();
            if (raidShopItemCell.id == itemId)
            {
                raidShopItemCell.inventoryText.text = "" + quantity;
            }   
        }
    }

    [System.Serializable]
    public class RaidShopItemInfo
    {
        public int id;
        public string name;
        public Sprite image;
        public int price;
        public int quantity;
        public string rsId;
    }
}
