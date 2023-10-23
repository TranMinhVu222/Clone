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

        Debug.Log(PlayerPrefs.GetString(itemInventoryKey));
        Debug.Log(PlayerPrefs.GetString(purchasedItemKey));
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
                    InventoryItemsData inventoryItemDataDict = itemData as InventoryItemsData;
                    inventoryItems[inventoryItemDataDict.item_id] = inventoryItemDataDict;
                }
            }
        }
    }

    private void SaveData<T, TType>(Dictionary<string, TType> dataDict, string key)
    {
        var itemDataList = new ItemsDataList<T>();
        itemDataList.itemsDataList = new List<T>();
        
        foreach (var kvp in dataDict)
        {
            T itemData = Activator.CreateInstance<T>();
            if (itemData is PurchasedItemsData purchasedData)
            {
                purchasedData.rsId = kvp.Key;
                purchasedData.itemCount = (int)(object)kvp.Value;
            }
            else if (itemData is InventoryItemsData inventoryData)
            {
                InventoryItemsData tempInventoryData = (InventoryItemsData)(object)kvp.Value;
                
                inventoryData.item_id = tempInventoryData.item_id;
                inventoryData.quantity = tempInventoryData.quantity;
            }
            itemDataList.itemsDataList.Add(itemData);
        }
    
        string serializedData = JsonUtility.ToJson(itemDataList);
        PlayerPrefs.SetString(key, serializedData);
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
public class ItemsDataList<T>
{
    public List<T> itemsDataList;
}