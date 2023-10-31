using System;
using System.Collections.Generic;
using UnityEngine;

public class UserInventoryManager : MonoBehaviour
{
    private const string userNameKey = "name";
    private const string tokenKey = "raidtoken";
    private const string itemInventoryKey = "inventory";
    private const string purchasedItemKey = "purchased";

    public Dictionary<string, InventoryItemsData> inventoryItems = new Dictionary<string, InventoryItemsData>();
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

    public InventoryItemsData GetInventoryUser(int itemId) { return GetITDictValue(inventoryItems, itemId.ToString()); }

    public void SetInventoryUser(string itemId, InventoryItemsData inventoryItemsData) { SetDictionaryValue<InventoryItemsData,InventoryItemsData>(inventoryItems,itemInventoryKey, itemId, inventoryItemsData); }

    public int GetPurchasedItem(string rsId) { return GetPIDictValue(purchasedItems, rsId); }

    public void SetPurchasedItem(string rsItem, int rsQuantity) { SetDictionaryValue  <PurchasedItemsData, int>(purchasedItems,purchasedItemKey, rsItem, rsQuantity); }

    private void LoadData<T>(string key)
    {
        string serializedData = PlayerPrefs.GetString(key, "");

        if (typeof(T) == typeof(PurchasedItemsData))
        {
            PurchasedItemsDataList purchasedItemsList = JsonUtility.FromJson<PurchasedItemsDataList>(serializedData);
            if (purchasedItemsList != null)
            {
                foreach (var purchasedItem in purchasedItemsList.purchased)
                {
                    purchasedItems[purchasedItem.rsId] = purchasedItem.itemCount;
                }
            }
        }
        else if (typeof(T) == typeof(InventoryItemsData))
        {
            InventoryItemsDataList inventoryItemsList = JsonUtility.FromJson<InventoryItemsDataList>(serializedData);
            if (inventoryItemsList != null)
            {
                foreach (var inventoryItem in inventoryItemsList.inventory)
                {
                    inventoryItems[inventoryItem.item_id] = inventoryItem;
                }
            }
        }
    }

    private void SaveData<T, TType>(Dictionary<string, TType> dataDict, string key)
    {
        if (typeof(T) == typeof(PurchasedItemsData))
        {
            PurchasedItemsDataList purchasedItemsList = new PurchasedItemsDataList();
            foreach (var kvp in dataDict)
            {
                PurchasedItemsData purchasedData = new PurchasedItemsData(kvp.Key,(int)(object)kvp.Value);
                purchasedItemsList.purchased.Add(purchasedData);
            }

            string serializedData = JsonUtility.ToJson(purchasedItemsList);
            PlayerPrefs.SetString(key, serializedData);
        }
        else if (typeof(T) == typeof(InventoryItemsData))
        {
            InventoryItemsDataList inventoryItemsList = new InventoryItemsDataList();

            foreach (var kvp in dataDict)
            {
                InventoryItemsData valueInventoryItem = (InventoryItemsData)(object)kvp.Value;
                InventoryItemsData inventoryData = new InventoryItemsData(valueInventoryItem.item_id, valueInventoryItem.quantity);
                inventoryItemsList.inventory.Add(inventoryData);
            }

            string serializedData = JsonUtility.ToJson(inventoryItemsList);
            PlayerPrefs.SetString(key, serializedData);
        }

        PlayerPrefs.Save();
    }
    
    private InventoryItemsData GetITDictValue(Dictionary<string, InventoryItemsData> dictionary, string id)
    {
        if (dictionary.ContainsKey(id))
        {
            return dictionary[id];
        }
        return new InventoryItemsData(id,0);
    }

    private int GetPIDictValue(Dictionary<string, int> dictionary, string key)
    {
        if (dictionary.ContainsKey(key))
        {
            return dictionary[key];
        }
        return 0;
    }

    private void SetDictionaryValue<T, TType>(Dictionary<string, TType> dictionary, string keyPlayerPrefs, string id, TType value)
    {   
        dictionary[id] = (TType)(object)value;
        SaveData<T, TType>(dictionary, keyPlayerPrefs);
    }
}

[Serializable]
public class InventoryItemsData
{
    public string item_id;
    public int quantity;
    public InventoryItemsData(){}
    public InventoryItemsData(string id, int quan)
    {
        item_id = id;
        quantity = quan;
    }
}

[Serializable]
public class PurchasedItemsData
{
    public string rsId;
    public int itemCount;
    public PurchasedItemsData() { }
    public PurchasedItemsData(string id, int quan)
    {
        rsId = id;
        itemCount = quan;
    }
}

[Serializable]
public class InventoryItemsDataList
{
    public List<InventoryItemsData> inventory = new List<InventoryItemsData>();
}

[Serializable]
public class PurchasedItemsDataList
{
    public List<PurchasedItemsData> purchased = new List<PurchasedItemsData>();
}