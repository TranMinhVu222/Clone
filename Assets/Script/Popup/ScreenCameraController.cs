using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCameraController : MonoBehaviour
{
    private static ScreenCameraController instance;
    public static  ScreenCameraController Instance {get => instance;}
   
    ScreenCamera screenCamera = new ScreenCamera();
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allow to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCamera(Camera cameraObj)
    {
        screenCamera.camera = cameraObj;
        Debug.Log(screenCamera.camera);
    }

    public void SetActiveCamera(bool check)
    {
        if (check)
        {
            screenCamera.ActiveUICamera();
        }
        else
        {
            screenCamera.InactiveUICamera();
        }
    }
}
