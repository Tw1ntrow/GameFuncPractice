using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Button[] tabButtons;
    public GameObject[] panels;
    public Color activeTabColor = Color.white;
    public Color inactiveTabColor = Color.gray;

    public void ShowTab(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
            tabButtons[i].image.color = (i == index) ? activeTabColor : inactiveTabColor;
        }
    }

    void Start()
    {
        ShowTab(0); // Å‰‚Ìƒ^ƒu‚ð•\Ž¦
    }
}