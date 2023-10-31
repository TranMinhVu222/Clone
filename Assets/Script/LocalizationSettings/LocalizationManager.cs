using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, string>> localizedText;
    private const string LanguagePlayerPrefsKey = "SelectedLanguage";
    public List<Screen> screenList = new List<Screen>();

    private string lang;

    private static LocalizationManager instance;
    public static LocalizationManager Instance { get => instance; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 Object allowed to exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        PlayerPrefs.GetString(LanguagePlayerPrefsKey, "english");
        CheckLocalizedAndLoadLocalizedText();
    }

    
    //{ "english", "french", "korean" }
    private void CheckLocalizedAndLoadLocalizedText()
    {
        SystemLanguage deviceLanguage = Application.systemLanguage;
        lang = deviceLanguage.ToString().ToLower();
        Debug.Log(lang);
        LoadLocalizedText(lang);
    }
    
    public void LoadLocalizedText(string lang)
    {
        localizedText = new Dictionary<string, Dictionary<string, string>>();

        TextAsset textAsset = Resources.Load<TextAsset>("LocalizationLang");
        if (textAsset != null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            XmlNodeList records = xmlDoc.SelectNodes("//records/record");
            foreach (XmlNode record in records)
            {
                string key = record.SelectSingleNode("NODE0").InnerText;
                localizedText[key] = new Dictionary<string, string>();

                if (record.SelectSingleNode(lang) != null)
                {
                    string value = record.SelectSingleNode(lang).InnerText;
                    localizedText[key][lang] = value;
                }
                else
                {
                    lang = "english";
                    string value = record.SelectSingleNode(lang).InnerText;
                    localizedText[key][lang] = value;
                }
            }
            
            foreach (var screen in screenList)
            {
                screen.ChangeLanguageText();
            }
        }
        else
        {
            Debug.LogError("Localization file not found.");
        }
    }

    

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key) && localizedText[key].ContainsKey(lang))
        {
            return localizedText[key][lang];
        }
        return key; // Return the key itself if not found
    }
}

[Serializable]
public class LocalizedText
{
    public Text uiText;
    public string key;
}