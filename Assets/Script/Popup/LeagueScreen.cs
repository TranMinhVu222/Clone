using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class LeagueScreen : Screen, IEnhancedScrollerDelegate
{
    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;

    private AdorableUser[] data;

    [SerializeField] private int setNumOfCell;
    [SerializeField] private Sprite[] birdArray;
    [SerializeField] private string[] nameArray;

    private void Start()
    {
        // Thiết lập delegate cho Enhanced Scroller
        scroller.Delegate = this;

        // Khởi tạo và nhập dữ liệu vào mảng data
        data = new AdorableUser[setNumOfCell];
        for (int i = 0; i < data.Length; i++)
        {
            int randomNumber = Random.Range(0, 1000);
            
            int rndNumber = Random.Range(0, birdArray.Length - 1);
            Sprite birdImage = birdArray[rndNumber];

            int rndNum = Random.Range(0, nameArray.Length - 1);
            string name = nameArray[rndNum];
            
            AdorableUser newData = new AdorableUser("Adorable " + name, (i + 1 < 10) ? "0" + (i+1) : "" + (i+1), birdImage, randomNumber);
            data[i] = newData;
        }
        
        scroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        // Trả về số lượng phần tử con trong ScrollView
        return data.Length;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        // Trả về kích thước của phần tử con tại vị trí dataIndex
        return 200f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // Tạo một CellView mới từ prefab
        CellView cellView = scroller.GetCellView(cellViewPrefab) as CellViewRow;
        // Gắn dữ liệu cho CellView
        cellView.SetData(data[dataIndex]);
        // Trả về CellView đã tạo
        return cellView;
    }
}