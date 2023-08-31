using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RaidShopDataManager : MonoBehaviour
{
    public List<RaidShopItem> raidShopItems = new List<RaidShopItem>();

    private static RaidShopDataManager instance;
    public static  RaidShopDataManager Instance {get => instance;}
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allow to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        string path = Application.dataPath + "/Data/RaidShop.json";
        
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);

            RaidShopItem[] items = ParseArray<RaidShopItem>(json);
            
            foreach (RaidShopItem item in items)
            {
                RaidShopItem newRaidShopItem = new RaidShopItem(item.item_id, item.price, item.quantity);
                raidShopItems.Add(newRaidShopItem);
            }
        }
        else
        {
            Debug.LogError("JSON file not found at the specified path: " + path);
        }
    }

    public RaidShopItem GetRaidShopItem(int id_index)
    {
        RaidShopItem raidShopItem = raidShopItems.Find(i => i.item_id == id_index);
        return raidShopItem;
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
    
    [System.Serializable]
    public class RaidShopItem
    {
        public int item_id;
        public int price;
        public int quantity;

        public RaidShopItem(int itemId, int itemPrice, int itemQuantity)
        {
            item_id = itemId;
            price = itemPrice;
            quantity = itemQuantity;
        }
    }
}
