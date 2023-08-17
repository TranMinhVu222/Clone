using UnityEngine;
using System.Collections;
using EnhancedScrollerDemos.GridSimulation;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
public class RaidShop : Screen, IEnhancedScrollerDelegate
{
    private SmallList<Data> _data;
    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;
    public int numberOfCellsPerRow = 2;
    void Start()
    {
        // tell the scroller that this script will be its delegate
        scroller.Delegate = this;

        // load in a large set of data
        LoadData();
    }
    private void LoadData()
    {
        _data = new SmallList<Data>();
        for (var i = 0; i < 8; i ++)
        {
            _data.Add(new Data() { someText = i.ToString() });
        }
        scroller.ReloadData();
    }
    
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)_data.Count / (float)numberOfCellsPerRow);
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 600f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;

        cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();
        
        cellView.SetData(ref _data, dataIndex * numberOfCellsPerRow);
        return cellView;
    }
}
