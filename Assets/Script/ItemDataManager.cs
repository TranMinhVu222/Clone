using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataManager: MonoBehaviour
{
    public List<Sprite> itemIconList = new List<Sprite>();
    public List<Item> itemList = new List<Item>();
    
    private static ItemDataManager instance;
    public static  ItemDataManager Instance {get => instance;}
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allow to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        string path = Application.dataPath + "/Data/Item.json";
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);

            ItemData[] items = ParseArray<ItemData>(json);

            foreach (ItemData item in items)
            {
                Item subItem = new Item(item.id, item.name, itemIconList[item.image_index]);
                itemList.Add(subItem);
            }
        }
        else
        {
            Debug.LogError("JSON file not found at the specified path: " + path);
        }
    }

    public Item GetItem(int id)
    {
        Item item = itemList.Find(i => i.id == id);
        return item;
    }

    private T[] ParseArray<T>(string json)
    {
        string newJson = "{\"array\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }
    
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
    
    [System.Serializable]
    public class ItemData
    {
        public int id;
        public string name;
        public int image_index;
    }
    
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
}