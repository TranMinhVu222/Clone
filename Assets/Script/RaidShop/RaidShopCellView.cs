using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using EnhancedUI;

public class RaidShopCellView: EnhancedScrollerCellView
{
        public RaidShopRowCellView[] rowCellViews;
        public void SetData(ref List<RaidShopManager.Product> data, int startingIndex)
        {
                // loop through the sub cells to display their data (or disable them if they are outside the bounds of the data)
                for (var i = 0; i < rowCellViews.Length; i++)
                {
                        // if the sub cell is outside the bounds of the data, we pass null to the sub cell
                        rowCellViews[i].SetData(startingIndex + i < data.Count ? data[startingIndex + i] : null);
                }
        }
}