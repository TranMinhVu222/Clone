using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, Dictionary<string, string>> localizedText;
    private const string LanguagePlayerPrefsKey = "SelectedLanguage";
    private string selectedLanguage = PlayerPrefs.GetString(LanguagePlayerPrefsKey, "en"); // Default language
    private int countSelectLang;
    
    private static LocalizationManager instance;
    public static LocalizationManager Instance { get => instance; }
    private void Awake()
    {
        if (instance != null)
        {
            
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadLocalizedText(selectedLanguage);
        LoadSavedLanguage();
    }

    private void CheckLocalizedAndLoadLocalizedText()
    {
        SystemLanguage deviceLanguage = Application.systemLanguage;
        SetLanguage(deviceLanguage.ToString().ToLower());
        LoadLocalizedText(deviceLanguage.ToString().ToLower());
    }

    private void SelectLocalized()
    {
        string[] languages = { "en", "vi", "fr" };
        countSelectLang = (countSelectLang % languages.Length) + 1;
        string lang = languages[countSelectLang - 1];
        SetLanguage(lang);
        LoadLocalizedText(lang);
        LoadSavedLanguage();
    }
        

    public void LoadLocalizedText(string lang)
    {
        localizedText = new Dictionary<string, Dictionary<string, string>>();

        TextAsset textAsset = Resources.Load<TextAsset>("Localization");
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
        if (localizedText.ContainsKey(key) && localizedText[key].ContainsKey(selectedLanguage))
        {
            return localizedText[key][selectedLanguage];
        }
        return key; // Return the key itself if not found
    }

    // Set the current language and save it to PlayerPrefs.
    public void SetLanguage(string language)
    {
        selectedLanguage = language;
        PlayerPrefs.SetString(LanguagePlayerPrefsKey, language);
    }

    // Load ngôn ngữ đã lưu từ PlayerPrefs
    private void LoadSavedLanguage()
    {
        if (PlayerPrefs.HasKey(LanguagePlayerPrefsKey))
        {
            selectedLanguage = PlayerPrefs.GetString(LanguagePlayerPrefsKey);
        }
    }
}
