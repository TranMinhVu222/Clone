using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class RaidShopManager : Screen, IEnhancedScrollerDelegate
{
    // public SmallList<RaidShopManager.Product> _data;
    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;
    public int numberOfCellsPerRow = 2;
    
    public List<Item> items;
    public List<RaidShopItem> itemRSList = new List<RaidShopItem>();
    public List<Product> products = new List<Product>();
    
    private static RaidShopManager instance;
    public static  RaidShopManager Instance {get => instance;}
    
    [System.Serializable]
    public class Product
    {
        public string name;
        public Sprite image;
        public int price;
        public int quantity;
    }
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allow to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        scroller.Delegate = this;
        
        string path = Application.dataPath + "/Data/RaidShop.json";
        items = ItemManager.Instance.itemList;
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);

            RaidShopItem[] items = ParseArray<RaidShopItem>(json);

            foreach (RaidShopItem item in items)
            {
                itemRSList.Add(SetItemData(item.item_id, item.price, item.quantity));
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy tệp JSON mục tại đường dẫn: " + path);
        }
        
        SetRaidShopItemInfo(items,itemRSList);
    }
    private RaidShopItem SetItemData(int id, int price, int quantity)
    {
        RaidShopItem item = new RaidShopItem(id, price, quantity);
        return item;
    }

    void SetRaidShopItemInfo(List<Item> items, List<RaidShopItem> raidShopItemData)
    {
        foreach (var raidShopItem in raidShopItemData)
        {
            var item = items.Find(i => i.id == raidShopItem.item_id);
            if (item != null)
            {
                var product = new Product
                {
                    name = item.name,
                    image = item.image,
                    price = raidShopItem.price,
                    quantity = raidShopItem.quantity
                };
                products.Add(product);
            }
        }
        scroller.ReloadData();
    }
    
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)itemRSList.Count / (float)numberOfCellsPerRow);
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 500f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        RaidShopCellView cellView = scroller.GetCellView(cellViewPrefab) as RaidShopCellView;

        cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();
        
        cellView.SetData(ref products, dataIndex * numberOfCellsPerRow);
        return cellView;
    }
    private T[] ParseArray<T>(string json)
    {
        string newJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }
    
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
