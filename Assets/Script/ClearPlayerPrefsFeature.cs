using System;
using UnityEditor;
using UnityEngine;

public class ClearPlayerPrefsFeature
{
    [MenuItem("VuTM/Clear Player Prefs")]
    private static void BuildAllAssetBundles()
    {
        try
        {
            PlayerPrefs.DeleteAll();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    } 
}