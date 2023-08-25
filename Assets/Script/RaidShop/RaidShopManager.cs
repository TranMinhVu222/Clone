using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EnhancedScrollerDemos.KeyControlGrid;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class RaidShopManager : Screen
{
    public GameObject containerPrefab;
    [SerializeField] private GridLayoutGroup layout;
    
    public List<Item> items;
    public List<RaidShopItem> itemRSList = new List<RaidShopItem>();
    public List<Product> products = new List<Product>();
    
    [System.Serializable]
    public class Product
    {
        public string name;
        public Sprite image;
        public int price;
        public int quantity;
    }
    
    public void Start()
    {
        string path = Application.dataPath + "/Data/RaidShop.json";
        items = ItemManager.Instance.itemList;
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);

            RaidShopItem[] items = ParseArray<RaidShopItem>(json);

            foreach (RaidShopItem item in items)
            {
                itemRSList.Add(item);
            }
        }
        else
        {
            Debug.LogError("JSON file not found at the specified path: " + path);
        }
        
        SetRaidShopItemInfo(items,itemRSList);
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

                GameObject instantiate = Instantiate(containerPrefab, layout.transform);
                instantiate.GetComponent<RaidShopCellContent>().SetData(product);
            }
        }
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
