using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendSearchItem : FriendItemView
{
    public Action<string,Sprite> OnClickRequestButton;
    public void OnRequestButton()
    {
        OnClickRequestButton?.Invoke(userNameText.text, avatarImage.sprite);
        Destroy(this.gameObject);
    }

}
