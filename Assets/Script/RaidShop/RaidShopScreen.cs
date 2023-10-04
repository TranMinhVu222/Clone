using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopScreen : Screen
{
    [SerializeField] private Text raidTokenText;
    [SerializeField] private GameObject raidShopCellPrefab;
    [SerializeField] private GridLayoutGroup layout;

    private List<GameObject> rsItemCellList = new List<GameObject>();
    private UserInventoryManager uimInstance;
    private void Start()
    {
        uimInstance = UserInventoryManager.Instance;
        ShowRaidShopItemInfo();
        ShowRaidToken();
        OnChangeColorPriceTextEvent();
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
            var itemCell = instantiate.GetComponent<RaidShopItemCell>();
            
            itemCell.SetData(product);

            itemCell.OnItemBought += HandleItemBought;
            
            rsItemCellList.Add(instantiate);
        }
    }

    public void OnClickInfoBtn()
    {
        int token = UserInventoryManager.Instance.GetToken();
        UserInventoryManager.Instance.SetToken(token + 1000);
        ShowRaidToken();
        OnChangeColorPriceTextEvent();
    }
    
    private void HandleItemBought(int itemId, int quantity)
    {
        ShowRaidToken();
        OnChangeColorPriceTextEvent();
        ShowInventoryUserText(itemId, quantity);
    }
    
    private void ShowRaidToken() { raidTokenText.text = "" + uimInstance.GetToken(); }
    

    private void ShowInventoryUserText(int itemId, int quantity)
    {
        foreach (var item in rsItemCellList)
        {
            var raidShopItemCell = item.GetComponent<RaidShopItemCell>();
            if (raidShopItemCell.GetId() == itemId)
            {
                raidShopItemCell.inventoryText.text = "" + quantity;
            }
        }
    }
    
    private void OnChangeColorPriceTextEvent()
    {
        foreach (var item in rsItemCellList)
        {
            var raidShopItemCell = item.GetComponent<RaidShopItemCell>();
            if (uimInstance.GetToken() < raidShopItemCell.GetPrice())
            {
                raidShopItemCell.priceItemText.color = new Color(1, 0.259434f, 0.3192778f, 1);
            }
            else
            {
                raidShopItemCell.priceItemText.color = new Color(1, 1, 1, 1f);
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
