using UnityEngine;

public class UserInventoryManager: MonoBehaviour
{
    private UserInfo userInfo;
    
    private const string userNameKey = "UserName";
    private const string tokenKey = "RaidToken";
    
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
    
    public string GetUserName()
    {
        PlayerPrefs.GetString(userNameKey, "Adorable");
        return PlayerPrefs.GetString(userNameKey);
    }

    public void SetUserName(string name)
    {
        PlayerPrefs.SetString(userNameKey, name);
    }
    
    public int GetToken()
    {
        PlayerPrefs.GetInt(tokenKey, 0);
        return PlayerPrefs.GetInt(tokenKey);
    }
    
    public void SetToken(int numToken)
    {
        PlayerPrefs.SetInt(tokenKey, numToken);
    }
    
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