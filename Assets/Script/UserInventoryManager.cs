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
    
    private LoadAndSetPlayerPrefs<InventoryItem> inventoryManager = new LoadAndSetPlayerPrefs<InventoryItem>();
    private LoadAndSetPlayerPrefs<PurchasedItem> purchasedItemManager = new LoadAndSetPlayerPrefs<PurchasedItem>();

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
        inventoryManager.LoadUserData(inventoryItems, itemInventoryKey);
        purchasedItemManager.LoadUserData(purchasedItems, purchasedItemKey);
    }

    public string GetUserName() { return PlayerPrefs.GetString(userNameKey, "Adorable"); }
    
    public void SetUserName(string name) { PlayerPrefs.SetString(userNameKey, name); }
    
    public int GetToken() { return PlayerPrefs.GetInt(tokenKey, 0); }
    
    public void SetToken(int numToken) { PlayerPrefs.SetInt(tokenKey, numToken); }
    
    public void SetInventoryUser(InventoryItem inventoryItem) {inventoryManager.SetUserData(inventoryItems,inventoryItem, itemInventoryKey); }
    
    public int GetInventoryUser(int itemId)
    {
        InventoryItem item = inventoryItems.Find(i => i.itemId == itemId);
        return item != null ? item.itemQuantity : 0;
    }
    
    public void SetPurchasedUser(PurchasedItem purchasedItem) {purchasedItemManager.SetUserData(purchasedItems,purchasedItem, purchasedItemKey); }

    public int GetPurchasedItem(string rsId)
    {
        PurchasedItem item = purchasedItems.Find(i => i.rsId == rsId);
        return item != null ? item.rsQuantity : 0;
    }
}