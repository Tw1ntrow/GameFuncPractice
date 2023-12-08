using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField]
    private Text title;

    private Action<int> onAct;
    private int id;
    public void SetView(string title, Action<int> onAct,int id)
    {
        this.title.text = title;
        this.onAct = onAct;
        this.id = id;
    }

    public void OnActButton()
    {
        onAct?.Invoke(id);
    }
}
