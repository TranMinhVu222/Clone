using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopScreen : Screen, IEnhancedScrollerDelegate
{
    [SerializeField] private Text raidTokenText;
    // [SerializeField] private GameObject raidShopCellPrefab;
    // [SerializeField] private GridLayoutGroup layout;

    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;
    
    private List<GameObject> rsItemCellList = new List<GameObject>();
    private RaidShopItemInfo[] data;
    
    private UserInventoryManager uimInstance;
    private void Start()
    {
        uimInstance = UserInventoryManager.Instance;
        scroller.Delegate = this;
        ShowRaidShopItemInfo();
        ShowRaidToken();
        OnChangeColorPriceTextEvent();
    }

    void  ShowRaidShopItemInfo()
    {
        data = new RaidShopItemInfo[RaidShopDataManager.Instance.raidShopItems.Count];

        for (int i = 0; i < RaidShopDataManager.Instance.raidShopItems.Count; i++)
        {
            ItemDataManager.Item itemInfo = ItemDataManager.Instance.GetItem(RaidShopDataManager.Instance.raidShopItems[i].item_id);

            RaidShopItemInfo product = new RaidShopItemInfo
            (
                itemInfo.id,
                itemInfo.name,
                itemInfo.image,
                RaidShopDataManager.Instance.raidShopItems[i].price,
                RaidShopDataManager.Instance.raidShopItems[i].quantity,
                RaidShopDataManager.Instance.raidShopItems[i].raidshop_id
            );
            
            data[i] = product;
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
    
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return RaidShopDataManager.Instance.raidShopItems.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 565.5f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        RaidShopItemCellView cellView = scroller.GetCellView(cellViewPrefab) as RaidShopItemCell;
        cellView.SetData(data[dataIndex]);
        cellView.HandleItemBought = HandleItemBought;
        return cellView;
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
        
        public RaidShopItemInfo(int idRS, string rsName, Sprite rsImage, int rsPrice, int rsQuantity, string rsIds)
        {
            id = idRS;
            name = rsName;
            image = rsImage;
            price = rsPrice;
            quantity = rsQuantity;
            rsId = rsIds;
        }
    }
}
