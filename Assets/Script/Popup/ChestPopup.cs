using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPopup : Popup
{
    [SerializeField] private UserInterface popupPrefab;

    public void OpenPopup()
    {
        PopupManager.Instance.TurnOnPopup(popupPrefab);
    }

    public void ClosePopup()
    {
        PopupManager.Instance.TurnOffPopup(popupPrefab);
    }
}
