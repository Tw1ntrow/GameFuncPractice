using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button panel;

    private int productId;
    private Action<int> onClick;

    public int ProductId { get => productId;  }

    public void DisplayPanel(int id,string Name,bool isSoldOut, Action<int> OnClick)
    {
        productId = id;
        text.text = Name;
        panel.interactable = !isSoldOut;
        onClick = OnClick;
    }

    public void UpdatePanel(bool isSoldOut)
    {
        panel.interactable = !isSoldOut;
    }
    public void OnClickPanel()
    {
        onClick?.Invoke(ProductId);
    }
}
