using System;
using System.Collections.Generic;
using UnityEngine;

public class HandleUserData
{
    private Dictionary<string, int> purchasedItems = new Dictionary<string, int>();

    public void LoadInventoryUserData(List<InventoryItem> items, string itemKey)
    {
        string data = PlayerPrefs.GetString(itemKey);
        string[] itemStrings = data.Split('\n');
        items.Clear();

        foreach (string itemString in itemStrings)
        {
            if (!string.IsNullOrEmpty(itemString))
            {
                string[] parts = itemString.Split(',');

                int itemId = int.Parse(parts[0]);
                int itemQuantity = int.Parse(parts[1]);
                InventoryItem item = new InventoryItem(itemId,itemQuantity );
                items.Add(item);
            }
        }
    }

    public void SetInventoryUserData(List<InventoryItem> items, InventoryItem newItem, string itemKey)
    {
        string data = "";

        InventoryItem existingItem = items.Find(item => item.itemId == newItem.itemId);
            
        if (existingItem != null)
        {
            existingItem.itemQuantity = newItem.itemQuantity;
        }
        else
        {
            items.Add(newItem);
        }

        data = string.Join("\n", items.ConvertAll( item =>
        {
            if (item is InventoryItem)
            {
                return $"{item.itemId},{item.itemQuantity}";
            }
            return "";
        }));

        PlayerPrefs.SetString(itemKey, data);
    }
    
    //Purchased Data
    
    void SavePurchasedItems(string key)
    {
        string serializedData = ConvertToString(purchasedItems);
        PlayerPrefs.SetString(key, serializedData);
    }
    
    public void LoadPurchasedItems(string key)
    {
        string serializedData = PlayerPrefs.GetString(key, "");
        
        purchasedItems = ConvertToDictionary(serializedData);
    }

    public void AddPurchasedItem(string item, int quantity, string key)
    {
        purchasedItems[item] = quantity;

        SavePurchasedItems(key);
    }

    public int GetPurchasedItemQuantity(string item)
    {
        if (purchasedItems.ContainsKey(item))
        {
            return purchasedItems[item];
        }
        return 0;
    }
    
    private string ConvertToString(Dictionary<string, int> dictionary)
    {
        string convertData = "";
        foreach (var kvp in dictionary)
        {
            convertData += kvp.Key + ":" + kvp.Value + ",";
        }
        return convertData;
    }
    
    private Dictionary<string, int> ConvertToDictionary(string data)
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        string[] dataArray = data.Split(',');
        foreach (string itemData in dataArray)
        {
            string[] parts = itemData.Split(':');
            if (parts.Length == 2)
            {
                string key = parts[0];
                int value;
                if (int.TryParse(parts[1], out value))
                {
                    dictionary[key] = value;
                }
            }
        }
        return dictionary;
    }
}

[Serializable]
public class InventoryItem
{
    public int itemId;
    public int itemQuantity;

    public InventoryItem(int id, int quantity)
    {
        itemId = id;
        itemQuantity = quantity;
    }
}