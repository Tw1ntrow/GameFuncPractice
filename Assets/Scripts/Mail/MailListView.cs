using System;
using System.Collections.Generic;
using UnityEngine;

public class MailListView : MonoBehaviour
{
    [SerializeField]
    private Transform mailContentParent;
    [SerializeField]
    private MailContent contntPrefab;

    private List<MailContent> mailContentList = new List<MailContent>();

    private Action<MailContent> onGetItem;
    private Action<List<MailContent>> onAllGetItem;

    /// <summary>
    /// メールを作成
    /// </summary>
    /// <param name="maildata">作成するメールのデータ</param>
    /// <param name="onGetItem"></param>
    /// <param name="onAllGetItem"></param>
    public void CreanteMailList(List<MailData> maildata, Action<MailContent> onGetItem, Action<List<MailContent>> onAllGetItem)
    {
        this.onGetItem = onGetItem;
        this.onAllGetItem = onAllGetItem;

        foreach (var data in maildata) 
        {
            MailContent mail = Instantiate<MailContent>(contntPrefab, mailContentParent);
            mail.SetMailContent(data, OnGetButton);
            mailContentList.Add(mail);
        }
    }

    /// <summary>
    /// アイテム獲得ボタン
    /// </summary>
    /// <param name="mailData"></param>
    public void OnGetButton(MailContent mailData)
    {
        // 管理クラスに受け取りの通知
        onGetItem?.Invoke(mailData);

        DialogManager.Instance.CreateDialog<CommonDialog>(new CommonDialogParameter("獲得！", $"ItemID:{mailData.MailData.id}を獲得しました"));

        // リスト上のメール項目削除
        Destroy(mailData.gameObject);



    }

    /// <summary>
    /// アイテム全て獲得ボタン
    /// </summary>
    public void OnGetAllItemButton()
    {
        // 管理クラスに受け取りの通知
        onAllGetItem?.Invoke(mailContentList);

        // 生成したメール項目を全て削除
        foreach(var mailContent in mailContentList)
        {
            Destroy(mailContent.gameObject);
        }
    }
}
