using UnityEngine;

public class RaidShopFeature: MonoBehaviour
{
    public void AddToken()
    {
        int currentRaidToken = PlayerPrefs.GetInt("RaidToken");
        PlayerPrefs.SetInt("RaidToken", currentRaidToken += 1000);
    }
}