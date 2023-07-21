using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : Screen
{
    private void Start()
    {
        // var rootCanvas = PopupManager.Instance.gameObject;
        // CanvasScaler canvasScaler = rootCanvas.GetComponent<CanvasScaler>();
        // float scaleFactor = canvasScaler.scaleFactor;
        //
        // RectTransform uiElement = gameObject.GetComponent<RectTransform>();
        //
        // CanvasScaler home = gameObject.GetComponent<CanvasScaler>();
        // home = canvasScaler;
        // uiElement.sizeDelta *= scaleFactor;
        // Debug.Log(scaleFactor +  " " + rootCanvas.name + home);
    }
    
    public void OpenScreen(UserInterface screenPrefab)
    {
        PopupManager.Instance.TurnOnPopup(screenPrefab);
    }

    public void CloseScreen(UserInterface screenPrefab)
    {
        PopupManager.Instance.TurnOffPopup(screenPrefab);
    }
    
    
}
