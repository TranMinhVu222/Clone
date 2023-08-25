using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemManager: MonoBehaviour
{
    public List<Sprite> itemIconList = new List<Sprite>();
    public List<Item> itemList = new List<Item>();
    
    private static ItemManager instance;
    public static  ItemManager Instance {get => instance;}
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allow to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void Start()
    {
        string path = Application.dataPath + "/Data/Item.json";
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);

            ItemData[] items = ParseArray<ItemData>(json);

            foreach (ItemData item in items)
            {
                itemList.Add(SetItemData(item.id, item.name, item.image_index));
            }
        }
        else
        {
            Debug.LogError("JSON file not found at the specified path: " + path);
        }
    }

    private Item SetItemData(int id, string name, int imageIndex)
    {
        Item item = new Item(id, name, itemIconList[imageIndex]);
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
}