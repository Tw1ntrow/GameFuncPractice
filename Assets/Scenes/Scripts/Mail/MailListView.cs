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
    /// ���[�����쐬
    /// </summary>
    /// <param name="maildata">�쐬���郁�[���̃f�[�^</param>
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
    /// �A�C�e���l���{�^��
    /// </summary>
    /// <param name="mailData"></param>
    public void OnGetButton(MailContent mailData)
    {
        // �Ǘ��N���X�Ɏ󂯎��̒ʒm
        onGetItem?.Invoke(mailData);

        DialogManager.Instance.CreateDialog<CommonDialog>(new CommonDialogParameter("�l���I", $"ItemID:{mailData.MailData.id}���l�����܂���"));

        // ���X�g��̃��[�����ڍ폜
        Destroy(mailData.gameObject);



    }

    /// <summary>
    /// �A�C�e���S�Ċl���{�^��
    /// </summary>
    public void OnGetAllItemButton()
    {
        // �Ǘ��N���X�Ɏ󂯎��̒ʒm
        onAllGetItem?.Invoke(mailContentList);

        // �����������[�����ڂ�S�č폜
        foreach(var mailContent in mailContentList)
        {
            Destroy(mailContent.gameObject);
        }
    }
}
