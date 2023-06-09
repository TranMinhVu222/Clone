using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class CellView : EnhancedScrollerCellView
{
    protected AdorableUser _adorableUser;
    
    public virtual void SetData(AdorableUser data)
    {
        _adorableUser = data;
    }
}
