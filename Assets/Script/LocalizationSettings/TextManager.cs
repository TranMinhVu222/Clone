using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public List<LocalizedText> textElements = new List<LocalizedText>();
    private int countSelectLang;
    private const string LanguagePlayerPrefsKey = "SelectedLanguage";
    
    private void Start()
    {
        UpdateText();
    }

    public void SelectLocalized()
    {
        string selectedLanguage = PlayerPrefs.GetString(LanguagePlayerPrefsKey, "english");
        Debug.Log("Selected Language: " + selectedLanguage);

        string[] languages = { "english", "french", "korean" };
        countSelectLang = (countSelectLang % languages.Length) + 1;
        string lang = languages[countSelectLang - 1];
        Debug.Log("Setting Language: " + lang);
    
        LocalizationManager.Instance.SetLanguage(lang);
        LocalizationManager.Instance.LoadLocalizedText(lang);
        UpdateText();
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