using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class LeagueCellView : EnhancedScrollerCellView
{
    protected LeagueUser _adorableUser;
    
    public virtual void SetData(LeagueUser data)
    {
        _adorableUser = data;
    }
}
