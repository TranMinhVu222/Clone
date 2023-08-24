using UnityEngine;
using System.Collections;
using EnhancedScrollerDemos.GridSimulation;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
public class RaidShop : Screen, IEnhancedScrollerDelegate
{
    public SmallList<RaidShopManager.Product> _data;
    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;
    public int numberOfCellsPerRow = 2;

    public GameObject rsManagerPrefab;
    
    void Awake()
    {
        // Instantiate(rsManagerPrefab);
    }
    void Start()
    {
        
        // tell the scroller that this script will be its delegate
        scroller.Delegate = this;
        
        // load in a large set of data
        // LoadData();
        Debug.Log(RaidShopManager.Instance.products.Count);
    }
    public void LoadData()
    {
        Debug.Log(_data);

        _data = new SmallList<RaidShopManager.Product>();
        foreach (var product in  RaidShopManager.Instance.products)
        {
            _data.Add(product);
        }
        scroller.ReloadData();
    }
    
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return Mathf.CeilToInt((float)_data.Count / (float)numberOfCellsPerRow);
        // return Mathf.CeilToInt((float)8 / (float)numberOfCellsPerRow);
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 500f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        RaidShopCellView cellView = scroller.GetCellView(cellViewPrefab) as RaidShopCellView;

        cellView.name = "Cell " + (dataIndex * numberOfCellsPerRow).ToString() + " to " + ((dataIndex * numberOfCellsPerRow) + numberOfCellsPerRow - 1).ToString();
        
        // cellView.SetData(ref _data, dataIndex * numberOfCellsPerRow);
        return cellView;
    }
}
