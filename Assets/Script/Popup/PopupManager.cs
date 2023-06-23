using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
  
   public List<UserInterface> recycleList = new List<UserInterface>();

   [SerializeField] private GameObject rootPopup;
   
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
   
   public void TurnOnPopup(UserInterface ui)
   {
      var uiView = GetRecycleList(ui);
      if (uiView == null)
      {
         var uiObject = Instantiate(ui,rootPopup.transform);
         // uiObject.transform.SetSiblingIndex((rootPopup.transform.childCount == 0) ? 0 : rootPopup.transform.childCount + 1);
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
