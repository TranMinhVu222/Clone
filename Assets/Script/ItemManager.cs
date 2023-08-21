using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ItemListWrapper
{
    public ItemTest[] items;
}

[System.Serializable]
public class ItemTest
{
    public int id;
    public string name;
    public int image_index;
}
public class ItemManager: MonoBehaviour
{
    public List<Sprite> itemIconList = new List<Sprite>();
    
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

    public void LoadData()
    {
        string path = Application.dataPath + "/Data/Item.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log(json);
            ItemListWrapper itemListWrapper = JsonUtility.FromJson<ItemListWrapper>(json);
                
            foreach (ItemTest item in itemListWrapper.items)
            {
                Debug.Log("Id Item: " + item.id);
                Debug.Log("Name Item: " + item.name);
                Debug.Log("Image Item: " + item.image_index);
            }
        }
    }
}