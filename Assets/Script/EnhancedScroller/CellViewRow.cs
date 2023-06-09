using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class CellViewRow : CellView
{
    private AdorableUser adorableUser;
    
    public Text userNameText;
    public Text orderNumberText;
    public Image avatatImage;
    public Text scoreText;
    
    public override void SetData(AdorableUser data)
    {
        // call the base SetData to link to the underlying _data
        base.SetData(data);

        // cast the data as rowData and store the reference
        adorableUser = data as AdorableUser;

        // update the UI with the data fields
        
        userNameText.text = _adorableUser.UserName;
        avatatImage.sprite = _adorableUser.UserAvatar;
        orderNumberText.text = _adorableUser.OrderNumber;
        scoreText.text = "" + _adorableUser.ScoreNumber;
    }
}
