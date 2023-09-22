using UnityEngine;

public class UserInventoryManager: MonoBehaviour
{
    private const string userNameKey = "UserName";
    private const string tokenKey = "RaidToken";
    private const string itemInventoryKey = "ItemInventory";
    
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

    public int GetInventoryUser(int id) { return PlayerPrefs.GetInt(itemInventoryKey + id,0); }

    public void SetInventoryUser(int id, int quantityPurchased) { PlayerPrefs.SetInt(itemInventoryKey + id, quantityPurchased);}
    
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
}