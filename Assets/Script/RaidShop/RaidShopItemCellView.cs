using System;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class RaidShopItemCellView: EnhancedScrollerCellView
{ 
        protected RaidShopScreen.RaidShopItemInfo raidShopItemInfo;
        
        protected Action<int, int> eventItemBought;

        public virtual void SetData(RaidShopScreen.RaidShopItemInfo data)
        {
                raidShopItemInfo = data;
        }

        public virtual void SetItemBought(Action<int, int> action)
        {
                eventItemBought += action;
        }
}