using UnityEngine;
using UnityEngine.UI;

public class ShowBudgetInfo: MonoBehaviour
{
      public Text raidTokenText;

      public void CurrentRaidToken(RaidUserManager.RaidUser data)
      {
            raidTokenText.text = "" + data.raidToken;
      }
}