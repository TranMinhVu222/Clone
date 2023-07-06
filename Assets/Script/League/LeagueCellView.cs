using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class LeagueCellView : EnhancedScrollerCellView
{
    protected LeagueUser leagueUser;
    
    public virtual void SetData(LeagueUser data)
    {
        leagueUser = data;
    }
}
