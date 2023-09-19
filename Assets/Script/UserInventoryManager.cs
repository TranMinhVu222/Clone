using System.Collections.Generic;
using UnityEngine;

public class UserInventoryManager: MonoBehaviour
{
    private const string userNameKey = "UserName";
    private const string tokenKey = "RaidToken";
    private const string itemInventoryKey = "ItemInventory";

    // private List<InventoryUser> inventoryUserList = new List<InventoryUser>();

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
    
    public string GetUserName() { return PlayerPrefs.GetString(userNameKey, "Adorable"); }
    
    public void SetUserName(string name) { PlayerPrefs.SetString(userNameKey, name); }
    
    public int GetToken() { return PlayerPrefs.GetInt(tokenKey, 0); }
    
    public void SetToken(int numToken) { PlayerPrefs.SetInt(tokenKey, numToken); }

    public int GetInventoryUser(int id) { return PlayerPrefs.GetInt(itemInventoryKey + id); }

    public void SetInventoryUser(int id, int quantityPurchased) { PlayerPrefs.SetInt(itemInventoryKey + id, quantityPurchased);}

    // public List<InventoryUser> InitInventoryUserList()
    // {
    //     foreach (var raidShopItem in RaidShopDataManager.Instance.raidShopItems)
    //     {
    //         int quantity = PlayerPrefs.GetInt(itemInventoryKey + raidShopItem.item_id,0);
    //
    //         InventoryUser inventoryUser = new InventoryUser(raidShopItem.item_id, quantity);
    //         
    //         inventoryUserList.Add(inventoryUser);
    //     }
    //
    //     return inventoryUserList;
    // }

    [System.Serializable]
    public class UserInfo
    {
        public string userName;
        public int raidToken;
        public UserInfo(string name, int token)
        {
            userName = name;
            raidToken = token;
        }
    }
    
    public class InventoryUser
    {
        public int itemId;
        public int itemQuantity;
        public InventoryUser(int id, int quantity)
        {
            itemId = id;
            itemQuantity = quantity;
        }
    }
}