using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField]
    private Text ItemName;
    [SerializeField]
    private Text ItemCount;
    public void ViewItem(string name,string count)
    {
        ItemName.text = name;
        ItemCount.text = count;
    }
}
