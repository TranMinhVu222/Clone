using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class LeagueScreen : Screen, IEnhancedScrollerDelegate
{
    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;

    private LeagueUser[] data;

    [SerializeField] private int setNumOfCell;
    [SerializeField] private Sprite[] birdArray;
    [SerializeField] private string[] nameArray;

    private float heightPrefab;

    [SerializeField] private Text leagueNameText;
    [SerializeField] private Text daysText;
    

    private void Start()
    {
        heightPrefab = cellViewPrefab.GetComponent<RectTransform>().rect.height;
        
        scroller.Delegate = this;
        
        data = new LeagueUser[setNumOfCell];
        for (int i = 0; i < data.Length; i++)
        {
            int randomNumber = Random.Range(0, 1000);
            
            int rndNumber = Random.Range(0, birdArray.Length - 1);
            Sprite birdImage = birdArray[rndNumber];

            int rndNum = Random.Range(0, nameArray.Length - 1);
            string name = nameArray[rndNum];
            
            LeagueUser newData = new LeagueUser("Adorable " + name, (i + 1 < 10) ? "0" + (i+1) : "" + (i+1), birdImage, randomNumber);
            data[i] = newData;
        }
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return data.Length;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return heightPrefab;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        LeagueCellView cellView = scroller.GetCellView(cellViewPrefab) as LeagueCellViewContent;
        cellView.SetData(data[dataIndex]);
        return cellView;
    }

    public override void ChangeLanguageText()
    {
        leagueNameText.text = LocalizationManager.Instance.GetLocalizedValue("vanilla_league");
        daysText.text = "4 " + LocalizationManager.Instance.GetLocalizedValue("days");
    }
}