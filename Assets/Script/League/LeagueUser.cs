using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeagueUser : MonoBehaviour
{
    //The name of user
    public string UserName { get; set; }
    
    //The user avatar's path to the sprite array which added from Assets/Arts/LeagueAssets/Bird
    public Sprite UserAvatar { get; set; }
    
    //The order number of user which is duplicate prefab order number
    public string OrderNumber { get; set; }
    
    //The score of user which random number range(0,1000)
    public int ScoreNumber { get; set; }
    
    public LeagueUser(string name, string order, Sprite avatar , int score)
    {
        UserName = name;
        OrderNumber = order;
        UserAvatar = avatar;
        ScoreNumber = score;
    }
}
