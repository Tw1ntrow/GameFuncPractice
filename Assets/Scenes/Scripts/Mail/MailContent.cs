using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class MailContent : MonoBehaviour
{
    [SerializeField]
    private Text body;
    [SerializeField]
    private Text title;
    //[SerializeField]
    //private ItemPanel itemPanel;
    [SerializeField]
    private Transform itemParent;

    private MailData mailData;
    private Action<MailContent> onGetButton;

    public MailData MailData { get => mailData; }

    public void SetMailContent(MailData mailData, Action<MailContent> onGetButton)
    {
        this.body.text = mailData.body;
        this.title.text = mailData.title;
        //itemPanel.ViewItem(mailData.itemId, mailData.itemCount.ToString(), 0);
        this.onGetButton = onGetButton;
        this.mailData = mailData;
    }

    public void OnClickGetBUtton()
    {
        onGetButton?.Invoke(this);
    }
}
