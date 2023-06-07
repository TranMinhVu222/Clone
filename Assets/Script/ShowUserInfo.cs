using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ShowUserInfo: MonoBehaviour
{
    public Text userNameText;
    public Text orderNumberText;
    public Image avatatImage;
    public Text scoreText;

    public void SetUI(User user)
    {
        userNameText.text = user.UserName;
        avatatImage.sprite = user.UserAvatar;
        orderNumberText.text = user.OrderNumber;
        scoreText.text = "" + user.ScoreNumber;
    }
}
