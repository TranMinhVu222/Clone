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

    private HandleUserData handleUserData = new HandleUserData();
    
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
        handleUserData.LoadInventoryUserData(inventoryItems, itemInventoryKey);
        handleUserData.LoadPurchasedItems(purchasedItemKey);
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
    
    public void SetInventoryUser(InventoryItem inventoryItem) { handleUserData.SetInventoryUserData(inventoryItems, inventoryItem, itemInventoryKey);}

    public int GetPurchasedItem(string rsId) { return handleUserData.GetPurchasedItemQuantity(rsId); }

    public void SetPurchasedItem(string rsItem, int rsQuantity) { handleUserData.AddPurchasedItem(rsItem, rsQuantity, purchasedItemKey); }
}