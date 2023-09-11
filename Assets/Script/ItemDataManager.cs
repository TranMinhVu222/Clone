using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager: DataManager<ItemDataManager>
{
    public List<Sprite> itemIconList = new List<Sprite>();
    public List<Item> itemList = new List<Item>();

    void Start()
    {
        string path = Application.dataPath + "/Data/Item.json";
       
        foreach (ItemData item in Initialize<ItemData>(path))
        {
            Item subItem = new Item(item.id, item.name, itemIconList[item.image_index]);
            itemList.Add(subItem);
        }
    }

    public Item GetItem(int id)
    {
        Item item = itemList.Find(i => i.id == id);
        return item;
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
        public int id;
        public string name;
        public Sprite image;
        public Item() {}
        public Item(int itemId, string itemName, Sprite itemImg)
        {
            id = itemId;
            name = itemName;
            image = itemImg;
        }
    }
}