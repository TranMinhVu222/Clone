using System.Collections.Generic;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopScreen : Screen, IEnhancedScrollerDelegate
{
    [SerializeField] private Text raidTokenText;

    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;

    private RaidShopItemInfo[] data;
    private List<GameObject> rsItemCellList = new List<GameObject>();

    private int numberOfCellsPerRow = 2;
    private float heightCell;

    public Text nameShop;
    
    private UserInventoryManager uimInstance;
    private void Start()
    {
        heightCell = cellViewPrefab.GetComponent<RectTransform>().rect.height;
        
        uimInstance = UserInventoryManager.Instance;
        scroller.Delegate = this;
        
        ShowRaidShopItemInfo();
        ShowRaidToken();
        OnChangeColorPriceText();
    }

    void ShowRaidShopItemInfo()
    {
        data = new RaidShopItemInfo[RaidShopDataManager.Instance.raidShopItems.Count];

        for (int i = 0; i < RaidShopDataManager.Instance.raidShopItems.Count; i++)
        {
            ItemDataManager.Item itemInfo = ItemDataManager.Instance.GetItem(RaidShopDataManager.Instance.raidShopItems[i].item_id);

            RaidShopItemInfo product = new RaidShopItemInfo
            (
                itemInfo.id,
                ChangeLanguageNameItemText(itemInfo.name),
                itemInfo.image,
                RaidShopDataManager.Instance.raidShopItems[i].price,
                RaidShopDataManager.Instance.raidShopItems[i].quantity,
                RaidShopDataManager.Instance.raidShopItems[i].raidshop_id
            );
            
            data[i] = product;
        }
        scroller.ReloadData();
    }

    public void OnClickInfoBtn()
    {
        int token = UserInventoryManager.Instance.GetToken();
        UserInventoryManager.Instance.SetToken(token + 1000);
        ShowRaidToken();
        OnChangeColorPriceText();
    }
    
    private void HandleItemBought(int itemId, int quantity)
    {
        ShowRaidToken();
        OnChangeColorPriceText();
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
    
    private void OnChangeColorPriceText()
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
        return Mathf.CeilToInt((float) RaidShopDataManager.Instance.raidShopItems.Count / (float)numberOfCellsPerRow);
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return heightCell;
    }
    
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        RaidShopItemRow cellView = scroller.GetCellView(cellViewPrefab) as RaidShopItemRow;
        
        cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow)+ " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1);
        
        cellView.SetData(data, dataIndex * numberOfCellsPerRow);

        foreach (var cell in cellView.raidShopItemCells)
        {
            rsItemCellList.Add(cell.gameObject);
            cell.OnItemBought += HandleItemBought;
        }
        
        OnChangeColorPriceText();
        
        return cellView;
    }
    
    private string ChangeLanguageNameItemText(string name)
    {
        return LocalizationManager.Instance.GetLocalizedValue(name.ToString().ToLower());
    }
    
    // This method is called to change the language-specific text on the RaidShopScreen.
    // It updates the text displayed on the screen to match the selected language.
    public override void ChangeLanguageText()
    {
        nameShop.text = LocalizationManager.Instance.GetLocalizedValue("sunshine_vendor");
        ShowRaidShopItemInfo();
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
