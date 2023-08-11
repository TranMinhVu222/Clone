using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRaidShop
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
