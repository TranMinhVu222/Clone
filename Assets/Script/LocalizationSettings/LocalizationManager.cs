using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    // Dictionary for storing localized text for different languages
    private Dictionary<string, Dictionary<string, string>> localizedText;
    // List of Screen objects that need to update their text
    public List<Screen> screenList = new List<Screen>();
    // The currently selected language
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
        
        // Check the device's system language and load the localized text
        CheckLocalizedAndLoadLocalizedText();
    }

    
    // Load localized text for a specified language
    private void CheckLocalizedAndLoadLocalizedText()
    {
        SystemLanguage deviceLanguage = Application.systemLanguage;
        lang = deviceLanguage.ToString().ToLower();
        LoadLocalizedText(lang);
    }
    
    // Load localized text for a specified language
    public void LoadLocalizedText(string lang)
    {
        localizedText = new Dictionary<string, Dictionary<string, string>>();
        
        // Attempt to load an XML file with localized text
        TextAsset textAsset = (TextAsset)Resources.Load("LocalizationLang", typeof(TextAsset));
        if (textAsset != null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);

            XmlNodeList records = xmlDoc.SelectNodes("//records/record");
            foreach (XmlNode record in records)
            {
                // Parse the XML data and populate the localizedText dictionary
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
    
    // Get the localized text for a given key
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