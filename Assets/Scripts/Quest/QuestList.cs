using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    [SerializeField]
    private QuestView questView;
    [SerializeField]
    private Transform questViewParent;

    [SerializeField]
    private QuestDetail questDetail;
    [SerializeField]
    private Transform questDetailParent;

    private List<QuestDeta> questDetaList = new List<QuestDeta>();
    void Start()
    {
        questDetaList = new List<QuestDeta> { new QuestDeta() { ID = 0, Title = "TitleID0", Description = "DescriptionID0" },
        new QuestDeta() { ID = 1, Title = "TitleID1", Description = "DescriptionID1" },
        new QuestDeta() { ID = 2, Title = "TitleID2", Description = "DescriptionID2" }};

        foreach(var quest in questDetaList)
        {
            QuestView questView = Instantiate<QuestView>(this.questView, questViewParent);
            questView.SetView(quest.Title, OnQuest, quest.ID);
        }
    }

    public void OnQuest(int id)
    {
        questDetail.SetView(questDetaList[id].Title, questDetaList[id].Description, id, OnAct);
    }

    public void OnAct(int id)
    {
        Debug.Log(id.ToSafeString());
    }

}
