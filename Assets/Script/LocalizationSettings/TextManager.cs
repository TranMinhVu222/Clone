using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public List<LocalizedText> textElements = new List<LocalizedText>();
    public List<Screen> screenList = new List<Screen>();
    private int countSelectLang;

    private void Start()
    {
        UpdateText();
    }

    public void SelectLocalized()
    {
        string[] languages = { "english", "french", "korean" };
        countSelectLang = (countSelectLang % languages.Length) + 1;
        string lang = languages[countSelectLang - 1];
        Debug.Log("Setting Language: " + lang);
    
        LocalizationManager.Instance.SetLanguage(lang);
        LocalizationManager.Instance.LoadLocalizedText(lang);
        UpdateText();
        foreach (var screen in screenList)
        {
            screen.ChangeLanguageText();
        }
    }
    
    private void UpdateText()
    {
        for (int i = 0; i < textElements.Count; i++)
        {
            textElements[i].uiText.text = LocalizationManager.Instance.GetLocalizedValue(textElements[i].key);
        }
    }
        
}

[Serializable]
public class LocalizedText
{
    public Text uiText;
    public string key;
}