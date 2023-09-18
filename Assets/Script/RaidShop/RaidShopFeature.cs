using UnityEngine;

public class RaidShopFeature: MonoBehaviour
{
    public void AddToken() => PlayerPrefs.SetInt("RaidToken", PlayerPrefs.GetInt("RaidToken") + 1000);
    
}