using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    [SerializeField]
    private MailListView mailListView;
    void Start()
    {
        Init();
    }

    private void Init()
    {
        // テスト用のMailDataリストを作成
        List<MailData> testMailData = new List<MailData> {
            new MailData(1, "Hello, this is body 1", "Title 1", "Item1", 10),
            new MailData(2, "Hello, this is body 2", "Title 2", "Item2", 20),
            new MailData(3, "Hello, this is body 3", "Title 3", "Item3", 30),
            new MailData(4, "Hello, this is body 4", "Title 4", "Item4", 40),
            new MailData(5, "Hello, this is body 5", "Title 5", "Item5", 50),
        };

        mailListView.CreanteMailList(testMailData, GetItemFromMailContent, GetAllItemsFromMailContentList);
    }

    private void GetItemFromMailContent(MailContent mailContent)
    {
        // アイテムを取得
        GetItem(mailContent.MailData.id);
    }

    private void GetAllItemsFromMailContentList(List<MailContent> mailContentList)
    {
        List<int> itemIds = new List<int>();
        foreach (MailContent mailContent in mailContentList)
        {
            itemIds.Add(mailContent.MailData.id);
        }

        // アイテムを取得
        GetAllItem(itemIds);
    }

    public void GetItem(int id)
    {
        Debug.Log($"{id}を獲得");
    }

    public void GetAllItem(List<int> ids)
    {
        foreach (int id in ids)
        {
            Debug.Log($"{id}を獲得");
        }
    }
}