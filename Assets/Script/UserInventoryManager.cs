using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

public class UserInventoryManager : MonoBehaviour
{
    private const string userNameKey = "UserName";
    private const string tokenKey = "RaidToken";
    private const string itemInventoryKey = "ItemInventory";
    private const string purchasedItemKey = "PurchasedItem";

    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();
    public Dictionary<string, int> purchasedItems = new Dictionary<string, int>();

    private static UserInventoryManager instance;
    public static UserInventoryManager Instance { get => instance; }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allowed to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadData<InventoryItemsData>(itemInventoryKey);
        LoadData<PurchasedItemsData>(purchasedItemKey);
    }

    public string GetUserName() { return PlayerPrefs.GetString(userNameKey, "Adorable"); }

    public void SetUserName(string name) { PlayerPrefs.SetString(userNameKey, name); }

    public int GetToken() { return PlayerPrefs.GetInt(tokenKey, 0); }

    public void SetToken(int numToken) { PlayerPrefs.SetInt(tokenKey, numToken); }

    public int GetInventoryUser(string itemId) { return GetDictionaryValue(inventoryItems, itemId); }

    public void SetInventoryUser(string itemId, int quantity) { SetDictionaryValue<InventoryItemsData>(inventoryItems,itemInventoryKey, itemId, quantity); }

    public int GetPurchasedItem(string rsId) { return GetDictionaryValue(purchasedItems, rsId); }

    public void SetPurchasedItem(string rsItem, int rsQuantity) { SetDictionaryValue<PurchasedItemsData>(purchasedItems,purchasedItemKey, rsItem, rsQuantity); }

    private void LoadData<T>(string key)
    {
        string serializedData = PlayerPrefs.GetString(key, "");
        ItemsDataList<T> itemsDataList = JsonUtility.FromJson<ItemsDataList<T>>(serializedData);
        if (itemsDataList != null)
        {
            foreach (var itemData in itemsDataList.itemsDataList)
            {
                if (itemData is PurchasedItemsData)
                {
                    PurchasedItemsData purchasedItemData = itemData as PurchasedItemsData;
                    purchasedItems[purchasedItemData.rsId] = purchasedItemData.itemCount;
                }
                else if (itemData is InventoryItemsData)
                {
                    InventoryItemsData inventoryItem = itemData as InventoryItemsData;
                    inventoryItems[inventoryItem.itemId.ToString()] = inventoryItem.itemQuantity;
                }
            }
        }
    }

    private void SaveData<T>(string key, Dictionary<string, int> dataDict)
    {
        ItemsDataList<T> itemDataList = new ItemsDataList<T>();
        itemDataList.itemsDataList = new List<T>();

        foreach (var kvp in dataDict)
        {
            T itemData;
            
            if (typeof(T) == typeof(PurchasedItemsData))
            {
                itemData = Activator.CreateInstance<T>();
                ((PurchasedItemsData)(object)itemData).rsId = kvp.Key;
                ((PurchasedItemsData)(object)itemData).itemCount = kvp.Value;
            }
            else if (typeof(T) == typeof(InventoryItemsData))
            {
                itemData = Activator.CreateInstance<T>();
                ((InventoryItemsData)(object)itemData).itemId = int.Parse(kvp.Key);
                ((InventoryItemsData)(object)itemData).itemQuantity = kvp.Value;
            }
            else
            {
                throw new ArgumentException("Error type");
            }

            itemDataList.itemsDataList.Add(itemData);
        }

        string serializedData = JsonUtility.ToJson(itemDataList);
        PlayerPrefs.SetString(key, serializedData);
        PlayerPrefs.Save();
    }

    private int GetDictionaryValue(Dictionary<string, int> dictionary, string key)
    {
        if (dictionary.ContainsKey(key))
        {
            return dictionary[key];
        }
        return 0;
    }

    private void SetDictionaryValue<T>(Dictionary<string, int> dictionary, string key, string id, int quantity)
    {
        dictionary[id] = quantity;
        SaveData<T>(key, dictionary);
    }
}

[Serializable]
public class InventoryItemsData
{
    public int itemId;
    public int itemQuantity;
}

[Serializable]
public class PurchasedItemsData
{
    public string rsId;
    public int itemCount;
}

[System.Serializable]
public class ItemsDataList<T>
{
    public List<T> itemsDataList;
}