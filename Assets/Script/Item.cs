using System;
using UnityEngine;

public class Item
{
    protected int id { get; set; }
    protected string name { get; set; }
    protected Sprite image { get; set; }
    
    public Item(int itemId, string itemName, Sprite itemImg)
    {
        id = itemId;
        name = itemName;
        image = itemImg;
    }
}
