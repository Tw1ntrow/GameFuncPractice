using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AccordionController : MonoBehaviour
{
    public RectTransform[] panels;
    public Image[] tabImages; // タイトル画像
    public Sprite openTabSprite; // タブが開いているときのタイトル
    public Sprite closedTabSprite; // タブが閉じているときのたいとる
    public float animationDuration = 0.5f; // アニメーション時間
    public float openHeight = 200f; // タブが開いているときの高さ

    public void TogglePanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            bool isOpen = panels[i].sizeDelta.y > 0;
            if (i == index && !isOpen)
            {
                // パネルを開くアニメーション
                OpenPanel(panels[i]);
                tabImages[i].sprite = openTabSprite;
            }
            else
            {
                // パネルを閉じるアニメーション
                ClosePanel(panels[i]);
                tabImages[i].sprite = closedTabSprite;
            }
        }
    }

    private void OpenPanel(RectTransform panel)
    {
        panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, openHeight), animationDuration);
        panel.gameObject.SetActive(true);
    }

    private void ClosePanel(RectTransform panel)
    {
        panel.DOSizeDelta(new Vector2(panel.sizeDelta.x, 0), animationDuration)
            .OnComplete(() => panel.gameObject.SetActive(false));
    }
}