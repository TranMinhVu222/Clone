using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : Screen
{
    // A list to store UI text elements with language localization keys
    public List<LocalizedText> textElements = new List<LocalizedText>();
    
    public Text pigDaysText;
    public Text raidDaysText;
    public void OpenScreen(UserInterface screenPrefab)
    {
        PopupManager.Instance.TurnOnPopup(screenPrefab);
    }

    public void CloseScreen(UserInterface screenPrefab)
    {
        PopupManager.Instance.TurnOffPopup(screenPrefab);
    }

    // This method is called to change the language-specific text on the HomeScreen.
    public override void ChangeLanguageText()
    {
        raidDaysText.text = "4 " + LocalizationManager.Instance.GetLocalizedValue("days");
        pigDaysText.text = "6 " + LocalizationManager.Instance.GetLocalizedValue("days");

        for (int i = 0; i < textElements.Count; i++)
        {
            // Retrieve translated text for the current element using the corresponding key
            textElements[i].uiText.text = LocalizationManager.Instance.GetLocalizedValue(textElements[i].key);
        }
    }
}

