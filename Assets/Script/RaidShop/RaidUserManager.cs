using UnityEngine;

public class RaidUserManager: MonoBehaviour
{
    private RaidUser raidUser;

    private static RaidUserManager instance;
    public static RaidUserManager Instance { get => instance; }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allow to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetData();
    }
    
    public RaidUser SetData()
    {
        string name;
        int token;
        
        if (!PlayerPrefs.HasKey("RaidToken") && !PlayerPrefs.HasKey("RaidUserName"))
        {
            
            PlayerPrefs.SetString("RaidUserName", "Adorable Dog");
            PlayerPrefs.SetInt("RaidToken", 0);
            
            return SetData();
        }
        
        name = PlayerPrefs.GetString("RaidUserName");
        token = PlayerPrefs.GetInt("RaidToken");

        raidUser = new RaidUser(name, token);

        return raidUser;
    }
    
    

    [System.Serializable]
    public class RaidUser
    {
        public string raidUserName;
        public int raidToken;
        public RaidUser(string name, int token)
        {
            raidUserName = name;
            raidToken = token;
        }
    }
}