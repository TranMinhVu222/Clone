using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RaidShopManager : MonoBehaviour
{
    public List<Item> items;
    public List<RaidShopItem> itemRSList = new List<RaidShopItem>();
    private List<Product> products = new List<Product>();
    
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
        int count = 0;
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
                // Debug.Log(products[count].name + " " +
                //           products[count].image.name + " " + 
                //           products[count].price + " " +
                //           products[count].quantity);
                // count++;
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
