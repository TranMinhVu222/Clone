using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UserInventoryManager: MonoBehaviour
{
    private const string userNameKey = "UserName";
    private const string tokenKey = "RaidToken";
    private const string itemInventoryKey = "ItemInventory";
    private const string purchasedItemKey = "PurchasedItem";

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public List<PurchasedItem> purchasedItems = new List<PurchasedItem>();

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
        LoadUserData<InventoryItem>(inventoryItems,itemInventoryKey);
        LoadUserData<PurchasedItem>(purchasedItems,purchasedItemKey);
    }

    public string GetUserName() { return PlayerPrefs.GetString(userNameKey, "Adorable"); }
    
    public void SetUserName(string name) { PlayerPrefs.SetString(userNameKey, name); }
    
    public int GetToken() { return PlayerPrefs.GetInt(tokenKey, 0); }
    
    public void SetToken(int numToken) { PlayerPrefs.SetInt(tokenKey, numToken); }
    
    public void LoadUserData<T>(List<T> items, string itemKey) where T : InventoryItemBase, new()
    {
        string data = PlayerPrefs.GetString(itemKey);
        string[] itemStrings = data.Split('\n');
        items.Clear();

        foreach (string itemString in itemStrings)
        {
            if (!string.IsNullOrEmpty(itemString))
            {
                string[] parts = itemString.Split(',');

                if (typeof(T) == typeof(InventoryItem))
                {
                    int itemId = int.Parse(parts[0]);
                    int itemQuantity = int.Parse(parts[1]);
                    T item = new T { itemId = itemId, itemQuantity = itemQuantity };
                    items.Add(item);
                }
                else if (typeof(T) == typeof(PurchasedItem))
                {
                    string rsId = parts[0];
                    int rsQuantity = int.Parse(parts[1]);
                    T item = new T { rsId = rsId, rsQuantity = rsQuantity };
                    items.Add(item);
                }
            }
        }
    }

    public void SetUserData<T>(List<T> items, T newItem) where T : InventoryItemBase
    {
        string itemKey = "";
        T existingItem = items.Find(item => item.itemId == newItem.itemId || item.rsId == newItem.rsId);

        if (existingItem != null)
        {
            existingItem.itemQuantity = newItem.itemQuantity;
        }
        else
        {
            items.Add(newItem);
        }

        string data = string.Join("\n", items.ConvertAll(item =>
        {
            if (item is InventoryItem)
            {
                itemKey = itemInventoryKey;
                return $"{item.itemId},{item.itemQuantity}";
            }
            else if (item is PurchasedItem)
            {
                itemKey = purchasedItemKey;
                return $"{item.rsId},{item.rsQuantity}";
            }
            return "";
        }));

        PlayerPrefs.SetString(itemKey, data);
    }

    public int GetInventoryUser(int itemId)
    {
        InventoryItem item = inventoryItems.Find(i => i.itemId == itemId);
        return item != null ? item.itemQuantity : 0;
    }

    public int GetPurchasedItem(string rsId)
    {
        PurchasedItem item = purchasedItems.Find(i => i.rsId == rsId);
        return item != null ? item.rsQuantity : 0;
    }

    [System.Serializable]
    public class InventoryItem : InventoryItemBase
    {
        public InventoryItem() { }
        public InventoryItem(int id, int quantity) : base(id, quantity) { }
    }

    [System.Serializable]
    public class PurchasedItem : InventoryItemBase
    {
        public PurchasedItem() { }
        public PurchasedItem(string id, int quantity) : base(id, quantity) { }
    }

    public class InventoryItemBase
    {
        public int itemId;
        public string rsId;
        public int itemQuantity;
        public int rsQuantity;
        public InventoryItemBase() { }
        public InventoryItemBase(int id, int quantity)
        {
            itemId = id;
            itemQuantity = quantity;
        }

        public InventoryItemBase(string id, int quantity)
        {
            rsId = id;
            rsQuantity = quantity;
        }
    }
}