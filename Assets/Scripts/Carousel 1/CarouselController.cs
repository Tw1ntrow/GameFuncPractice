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
        private Button nextButton; // ���փ{�^��
        [SerializeField]
        public Button prevButton; // �O�փ{�^��

        private int currentPageIndex = 0; // ���݂̃y�[�W�C���f�b�N�X
        private float pageWidth; // �e�y�[�W�̕�

        void Start()
        {
            // �ŏ��̃y�[�W�̕����v�Z�i���ׂẴy�[�W�͓������Ɖ���j
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