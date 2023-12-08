using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Carousel
{
    public class CarouselController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private Button nextButton; // 次へボタン
        [SerializeField]
        public Button prevButton; // 前へボタン

        private int currentPageIndex = 0; // 現在のページインデックス
        private float pageWidth; // 各ページの幅

        void Start()
        {
            // 最初のページの幅を計算（すべてのページは同じ幅と仮定）
            if (contentPanel.childCount > 0)
            {
                pageWidth = contentPanel.GetChild(0).GetComponent<RectTransform>().rect.width;
            }
            UpdateButtonInteractivity();
        }

        public void ShowNextPage()
        {
            if (currentPageIndex < contentPanel.childCount - 1)
            {
                currentPageIndex++;
                Vector2 newPosition = new Vector2(-pageWidth * currentPageIndex, contentPanel.anchoredPosition.y);
                contentPanel.anchoredPosition = newPosition;
                UpdateButtonInteractivity();
            }
        }

        public void ShowPreviousPage()
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                Vector2 newPosition = new Vector2(-pageWidth * currentPageIndex, contentPanel.anchoredPosition.y);
                contentPanel.anchoredPosition = newPosition;
                UpdateButtonInteractivity();
            }
        }

        private void UpdateButtonInteractivity()
        {
            nextButton.interactable = currentPageIndex < contentPanel.childCount - 1;
            prevButton.interactable = currentPageIndex > 0;
        }
    }
}