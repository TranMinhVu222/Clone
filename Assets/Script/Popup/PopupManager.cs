using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
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
}
