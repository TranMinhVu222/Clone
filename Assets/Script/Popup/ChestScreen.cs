using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScreen : Screen
{
    [SerializeField] private UserInterface screenPrefab;
    
    public void OpenScreen()
    {
        PopupManager.Instance.TurnOnPopup(screenPrefab);
    }

    public void CloseScreen()
    {
        PopupManager.Instance.TurnOffPopup(screenPrefab);
    }
}
