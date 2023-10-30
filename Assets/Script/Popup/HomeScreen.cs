using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : Screen
{
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

    public override void ChangeLanguageText()
    {
        raidDaysText.text = "4 " + LocalizationManager.Instance.GetLocalizedValue("days");
        pigDaysText.text = "6 " + LocalizationManager.Instance.GetLocalizedValue("days");

        Debug.Log(textElements.Count);
        
        for (int i = 0; i < textElements.Count; i++)
        {
            textElements[i].uiText.text = LocalizationManager.Instance.GetLocalizedValue(textElements[i].key);
        }
    }
}

