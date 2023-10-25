using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public List<LocalizedText> textElements = new List<LocalizedText>();

    private void Start()
    {
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