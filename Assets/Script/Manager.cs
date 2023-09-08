using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Manager<T> : MonoBehaviour where T : Manager<T>
{
    private bool isDontDestroyOnLoadSet = false;

    private static T instance;
    public static T Instance { get => instance; }
    
    protected Manager()
    {
        instance = this as T;
    }
    
    protected List<TData> InitializeList<TData>()
    {
        return new List<TData>();
    }
    protected List<TypeData> Initialize<TypeData>(string path) where TypeData : new()
    {
        List<TypeData> dataList = InitializeList<TypeData>();
        if (File.Exists(path))
        {
            String json = File.ReadAllText(path);

            TypeData[] typeDataArray = ParseArray<TypeData>(json);

            for (int i = 0; i < typeDataArray.Length; i++)
            {
                dataList.Add(typeDataArray[i]);
            }
        }
        else
        {
            Debug.LogError("JSON file not found at the specified path: " + path);
        }

        return dataList;
    }

    
    
    protected void Awake()
    {
        if (!isDontDestroyOnLoadSet)
        {
            DontDestroyOnLoad(gameObject);
            isDontDestroyOnLoadSet = true;
        }
    }
    
    private U[] ParseArray<U>(string json)
    {
        string newJson = "{\"array\":" + json + "}";
        Wrapper<U> wrapper = JsonUtility.FromJson<Wrapper<U>>(newJson);
        return wrapper.array;
    }
    
    [System.Serializable]
    private class Wrapper<U>
    {
        public U[] array;
    }
}