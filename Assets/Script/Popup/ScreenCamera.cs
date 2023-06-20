
using Assets.Script.Popup;
using UnityEngine;

public class ScreenCamera: UICamera, IInteractable
{
    public Camera camera;
    
    public void ActiveUICamera()
    {
        camera.gameObject.SetActive(true);
    }

    public void InactiveUICamera()
    {
        camera.gameObject.SetActive(false);
    }
}
