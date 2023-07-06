using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class LeagueCellViewContent : LeagueCellView
{
<<<<<<<< HEAD:Assets/Script/League/LeagueCellViewContent.cs
    private LeagueUser leagueUser;
========
    private LeagueUser adorableUser;
>>>>>>>> team_develop:Assets/Script/Popup/League/LeagueCellViewContent.cs
    
    public Text userNameText;
    public Text orderNumberText;
    public Image avatatImage;
    public Text scoreText;
    
    public override void SetData(LeagueUser data)
    {
        // call the base SetData to link to the underlying _data
        base.SetData(data);

        // cast the data as rowData and store the reference
<<<<<<<< HEAD:Assets/Script/League/LeagueCellViewContent.cs
        leagueUser = data as LeagueUser;
========
        adorableUser = data as LeagueUser;
>>>>>>>> team_develop:Assets/Script/Popup/League/LeagueCellViewContent.cs

        // update the UI with the data fields
        
        userNameText.text = leagueUser.UserName;
        avatatImage.sprite = leagueUser.UserAvatar;
        orderNumberText.text = leagueUser.OrderNumber;
        scoreText.text = "" + leagueUser.ScoreNumber;
    }
}
