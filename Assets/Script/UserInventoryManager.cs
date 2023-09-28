using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

public class UserInventoryManager: MonoBehaviour
{
    private const string userNameKey = "UserName";
    private const string tokenKey = "RaidToken";
    private const string itemInventoryKey = "ItemInventory";
    private const string purchasedItemKey = "PurchasedItem";

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public Dictionary<string, int> purchasedItems = new Dictionary<string, int>();

    private static UserInventoryManager instance;
    public static UserInventoryManager Instance { get => instance; }

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
        LoadInventoryUserData(itemInventoryKey);
        LoadPurchasedItems(purchasedItemKey);
    }

    public string GetUserName() { return PlayerPrefs.GetString(userNameKey, "Adorable"); }
    
    public void SetUserName(string name) { PlayerPrefs.SetString(userNameKey, name); }
    
    public int GetToken() { return PlayerPrefs.GetInt(tokenKey, 0); }
    
    public void SetToken(int numToken) { PlayerPrefs.SetInt(tokenKey, numToken); }

    public int GetInventoryUser(int itemId)
    {
        InventoryItem item = inventoryItems.Find(i => i.itemId == itemId);
        return item != null ? item.itemQuantity : 0;
    }
    
    public void SetInventoryUser(InventoryItem inventoryItem) { SetInventoryUserData(inventoryItem, itemInventoryKey);}

    public int GetPurchasedItem(string rsId) { return GetPurchasedItemQuantity(rsId); }

    public void SetPurchasedItem(string rsItem, int rsQuantity) { AddPurchasedItem(rsItem, rsQuantity, purchasedItemKey); }
    
    
    //Feature Logic
    
    private void LoadInventoryUserData(string itemKey)
    {
        string data = PlayerPrefs.GetString(itemKey);
        if (!string.IsNullOrEmpty(data))
        {
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(data);
            if (inventoryData != null)
            {
                inventoryItems = inventoryData.inventory;
            }
        }
    }

    private void SetInventoryUserData(InventoryItem newItem, string itemKey)
    {
        if (inventoryItems == null)
        {
            inventoryItems = new List<InventoryItem>();
        }

        int existingIndex = inventoryItems.FindIndex(item => item.itemId == newItem.itemId);

        if (existingIndex >= 0)
        {
            inventoryItems[existingIndex] = newItem;
        }
        else
        {
            inventoryItems.Add(newItem);
        }

        InventoryData inventoryData = new InventoryData();
        inventoryData.inventory = inventoryItems;

        string data = JsonUtility.ToJson(inventoryData);
        PlayerPrefs.SetString(itemKey, data);
        PlayerPrefs.Save();
    }

    // Purchased Data

    void SavePurchasedItems(string key)
    {
        PurchasedItemsList purchasedItemsData = new PurchasedItemsList();
        purchasedItemsData.purchasedItems = new List<PurchasedItemsData>();
        
        foreach (var kvp in purchasedItems)
        {
            PurchasedItemsData itemData = new PurchasedItemsData     {
                rsId = kvp.Key,
                itemCount = kvp.Value
            };
            purchasedItemsData.purchasedItems.Add(itemData);
        }

        string serializedData = JsonUtility.ToJson(purchasedItemsData);
        PlayerPrefs.SetString(key, serializedData);
        PlayerPrefs.Save();
    }

    private void LoadPurchasedItems(string key)
    {
        string serializedData = PlayerPrefs.GetString(key, "");
        if (!string.IsNullOrEmpty(serializedData))
        {
            PurchasedItemsList purchasedItemsData = JsonUtility.FromJson<PurchasedItemsList>(serializedData);
            if (purchasedItemsData != null)
            {
                purchasedItems = purchasedItemsData.purchasedItems.ToDictionary(itemData => itemData.rsId, itemData => itemData.itemCount);
            }
        }
    }

    private void AddPurchasedItem(string item, int quantity, string key)
    {
        purchasedItems[item] = quantity;
        SavePurchasedItems(key);
    }

    private int GetPurchasedItemQuantity(string item)
    {
        if (purchasedItems.ContainsKey(item))
        {
            return purchasedItems[item];
        }
        return 0;
    }
}

[Serializable]
public class InventoryItem
{
    public int itemId;
    public int itemQuantity;

    public InventoryItem(int id, int quantity)
    {
        itemId = id;
        itemQuantity = quantity;
    }
}

[Serializable]
public class InventoryData
{
    public List<InventoryItem> inventory;
}

[Serializable]
public class PurchasedItemsData
{
    public string rsId;
    public int itemCount;
}

[System.Serializable]
public class PurchasedItemsList
{
    public List<PurchasedItemsData> purchasedItems;
}