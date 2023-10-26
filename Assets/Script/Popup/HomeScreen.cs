using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : Screen
{
    public Text daysText;
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
        daysText.text = "6 " + LocalizationManager.Instance.GetLocalizedValue("days");
        raidDaysText.text = "4 " + LocalizationManager.Instance.GetLocalizedValue("days");;
    }
}
