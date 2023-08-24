using System;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id { get; set; }
    public string name { get; set; }
    public Sprite image { get; set; }
    
    public Item(int itemId, string itemName, Sprite itemImg)
    {
        id = itemId;
        name = itemName;
        image = itemImg;
    }
}
