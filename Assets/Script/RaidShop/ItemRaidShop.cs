using System.Collections;
using System.Collections.Generic;
using Assets.Script;
using UnityEngine;

public class ItemRaidShop: Item
{
    public int itemID { get; }

    public int itemPrice { get; }

    public int itemQuantiy { get; }
    
    public ItemRaidShop(int id, int price, int quantity)
    {
        itemID = id;
        itemPrice = price;
        itemQuantiy = quantity;
    }
}
