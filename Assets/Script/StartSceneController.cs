using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [SerializeField] private UserInterface homeScreen;
    void Start()
    {
        //init ads Sdk, other stuff
        
        
        //Init UI
        //1. Init PopupManager len scene
        GameObject popupManager = Instantiate(Resources.Load<GameObject>("PopupManager"));

        //2. PopupManager.Instance.OpenPopup(HomeScreen)
        if (popupManager != null)
        {
            PopupManager.Instance.TurnOnPopup(homeScreen);    
        }
        //Init Item Manager
        GameObject itemManager = Instantiate(Resources.Load<GameObject>("ItemManager"));
    }
}
