using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private Text ItemName;
    [SerializeField]
    private Text ItemCount;

    private Action<int> onClose;
    private int itemId;

    public void ViewItem(string name,string count,int itemid ,Action<int> OnCloseButton = null)
    {
        ItemName.text = name;
        ItemCount.text = $"Å~{count}";
        itemId = itemid;
        onClose = OnCloseButton;
    }

    public void OnClickButton()
    {
        onClose?.Invoke(itemId);
    }
}
