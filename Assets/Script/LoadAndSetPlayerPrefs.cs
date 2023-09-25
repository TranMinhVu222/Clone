using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadAndSetPlayerPrefs<T> where T : InventoryItemBase, new()
{
    public List<T> items = new List<T>();

    public void LoadUserData(List<T> items, string itemKey)
    {
        string data = PlayerPrefs.GetString(itemKey);
        string[] itemStrings = data.Split('\n');
        items.Clear();

        foreach (string itemString in itemStrings)
        {
            if (!string.IsNullOrEmpty(itemString))
            {
                string[] parts = itemString.Split(',');

                if (typeof(T) == typeof(InventoryItem))
                {
                    int itemId = int.Parse(parts[0]);
                    int itemQuantity = int.Parse(parts[1]);
                    T item = new T { itemId = itemId, itemQuantity = itemQuantity };
                    items.Add(item);
                }
                else if (typeof(T) == typeof(PurchasedItem))
                {
                    string rsId = parts[0];
                    int rsQuantity = int.Parse(parts[1]);
                    T item = new T { rsId = rsId, rsQuantity = rsQuantity };
                    items.Add(item);
                }
            }
        }
    }

    public void SetUserData(List<T> items, T newItem, string itemKey)
    {
        string data = "";

        if (typeof(T) == typeof(InventoryItem))
        {
            T existingItem = items.Find(item => item.itemId == newItem.itemId);
            
            if (existingItem != null)
            {
                existingItem.itemQuantity = newItem.itemQuantity;
            }
            else
            {
                items.Add(newItem);
            }

            data = string.Join("\n", items.ConvertAll(item =>
            {
                if (item is InventoryItem)
                {
                    return $"{item.itemId},{item.itemQuantity}";
                }
                return "";
            }));
        }
        else if (typeof(T) == typeof(PurchasedItem))
        {
            T existingItem = items.Find(item => item.rsId == newItem.rsId);
            if (existingItem != null)
            {
                existingItem.rsQuantity = newItem.rsQuantity;
            }
            else
            {
                items.Add(newItem);
            }

            data = string.Join("\n", items.ConvertAll(item =>
            {
                if (item is PurchasedItem)
                {
                    return $"{item.rsId},{item.rsQuantity}";
                }
                return "";
            }));
        }

        PlayerPrefs.SetString(itemKey, data);
    }
}

[Serializable]
public class InventoryItem : InventoryItemBase
{
    public InventoryItem() { }
    public InventoryItem(int id, int quantity) : base(id, quantity) { }
}

[Serializable]
public class PurchasedItem : InventoryItemBase
{
    public PurchasedItem() { }
    public PurchasedItem(string id, int quantity) : base(id, quantity) { }
}

[Serializable]
public class InventoryItemBase
{
    public int itemId;
    public string rsId;
    public int itemQuantity;
    public int rsQuantity;

    public InventoryItemBase() { }

    public InventoryItemBase(int id, int quantity)
    {
        itemId = id;
        itemQuantity = quantity;
    }

    public InventoryItemBase(string id, int quantity)
    {
        rsId = id;
        rsQuantity = quantity;
    }
}