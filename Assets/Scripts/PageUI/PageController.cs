using UnityEngine;
using UnityEngine.UI;

public class PageController : MonoBehaviour
{
    [SerializeField]
    private Image[] pages;
    [SerializeField]
    private Image[] indicators;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    public Button previousButton;
    // インジゲーターの色
    public Color activeIndicatorColor = Color.blue;
    public Color inactiveIndicatorColor = Color.gray;
    private int currentPageIndex = 0;

    void Start()
    {
        ShowPage(currentPageIndex);
    }

    public void ShowNextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            ShowPage(currentPageIndex);
        }
    }

    public void ShowPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ShowPage(currentPageIndex);
        }
    }

    private void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(i == index);
            indicators[i].color = (i == index) ? activeIndicatorColor : inactiveIndicatorColor;
        }

        UpdateButtonInteractivity();
    }

    private void UpdateButtonInteractivity()
    {
        nextButton.interactable = (currentPageIndex < pages.Length - 1);
        previousButton.interactable = (currentPageIndex > 0);
    }
}