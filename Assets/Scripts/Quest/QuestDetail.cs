using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDetail : MonoBehaviour
{
    [SerializeField]
    private Text title;
    [SerializeField] 
    private Text description;

    private int id;
    private Action<int> onActButton;
    public void SetView(string title, string description,int id,Action<int> OnActButton)
    {
        this.title.text = title;
        this.description.text = description;
        this.id = id;
        this.onActButton = OnActButton;
    }

    public void OnActButton()
    {
        onActButton?.Invoke(id);
    }

    public void OnCancel()
    {
        Destroy(gameObject);
    }
}
