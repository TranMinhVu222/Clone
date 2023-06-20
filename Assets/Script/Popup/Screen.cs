using System.Collections;
using System.Collections.Generic;
using Assets.Script.Popup;
using UnityEngine;

public class Screen: UserInterface
{
    public void ShowScreen()
    {
        ScreenCameraController.Instance.SetActiveCamera(true);
    }

    public void HiddenScreen()
    {
        ScreenCameraController.Instance.SetActiveCamera(false);
    }
}
