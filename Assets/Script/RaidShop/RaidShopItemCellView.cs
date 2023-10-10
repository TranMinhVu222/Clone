using System;
using System.Collections.Generic;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class RaidShopItemCellView: EnhancedScrollerCellView
{
        public RaidShopItemCell[] raidShopItemCells;

        public void SetData(RaidShopScreen.RaidShopItemInfo[] data, int startingIndex)
        {
                for (var i = 0; i < raidShopItemCells.Length; i++)
                {
                        raidShopItemCells[i].SetData(startingIndex + i < data.Length ? data[startingIndex + i] : null);
                }
        }

        public void HandleEvent(Action<int, int> action)
        {
                for (var i = 0; i < raidShopItemCells.Length; i++)
                {
                        raidShopItemCells[i].OnItemBought += action;
                }
        }
}