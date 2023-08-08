using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidShopManager : MonoBehaviour
{
    private static RaidShopManager instance;
    public static  RaidShopManager Instance {get => instance;}

    public List<Sprite> itemIconList = new List<Sprite>();
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allow to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
