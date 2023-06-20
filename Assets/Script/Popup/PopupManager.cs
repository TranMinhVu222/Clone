using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
   [SerializeField] private Camera cameraScreen;
   
   public List<UserInterface> recycleList = new List<UserInterface>();

   private static PopupManager instance;
   public static  PopupManager Instance {get => instance;}
   
   void Awake()
   {
      if (instance != null)
      {
         Debug.LogError("Only 1 Object allow to exist");
      }
      instance = this;
      DontDestroyOnLoad(gameObject);
   }

   private void Start()
   {
      ScreenCameraController.Instance.SetCamera(cameraScreen);
   }

   public void TurnOnPopup(UserInterface ui)
   {
      var uiView = GetRecycleList(ui);
      if (uiView == null)
      {
         var uiObject = Instantiate(ui,gameObject.transform);
         uiObject.transform.SetSiblingIndex(gameObject.transform.childCount - 2);
         recycleList.Add(uiObject);
      }
      else
      {
         uiView.gameObject.SetActive(true);
      }
   }

   public void TurnOffPopup(UserInterface ui)
   {
      var uiView = GetRecycleList(ui);
      uiView.gameObject.SetActive(false);
   }

   public UserInterface GetRecycleList(UserInterface ui)
   {
      for (var i = 0; i < recycleList.Count; i++)
      {
         if (recycleList[i].identifier == ui.identifier)
         {
            return recycleList[i];
         }
      }
      return null;
   }
}
