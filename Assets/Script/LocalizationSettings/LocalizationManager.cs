using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, string>> localizedText;
    private const string LanguagePlayerPrefsKey = "SelectedLanguage";

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
        // CheckLocalizedAndLoadLocalizedText();
        
        LoadLocalizedText(PlayerPrefs.GetString(LanguagePlayerPrefsKey,"english"));
    }

    // private void CheckLocalizedAndLoadLocalizedText()
    // {
    //     SystemLanguage deviceLanguage = Application.systemLanguage;
    //     string lang = deviceLanguage.ToString();
    //     SetLanguage(lang);
    //     LoadLocalizedText(lang);
    //     LoadSavedLanguage();
    // }
    
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
            }
        }
        else
        {
            Debug.LogError("Localization file not found.");
        }
    }

    public string GetLocalizedValue(string key)
    {
        string selectedLanguage = PlayerPrefs.GetString(LanguagePlayerPrefsKey, "english");
        if (localizedText.ContainsKey(key) && localizedText[key].ContainsKey(selectedLanguage))
        {
            return localizedText[key][PlayerPrefs.GetString(LanguagePlayerPrefsKey,"english")];
        }
        return key; // Return the key itself if not found
    }

    // Set the current language and save it to PlayerPrefs.
    public void SetLanguage(string language)
    {
        PlayerPrefs.SetString(LanguagePlayerPrefsKey, language);
    }
}