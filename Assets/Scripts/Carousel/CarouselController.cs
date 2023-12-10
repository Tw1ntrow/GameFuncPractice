using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class CarouselController : MonoBehaviour, IEndDragHandler
{
    [SerializeField]
    private CarouselItem itemPrefab;
    [SerializeField]
    private Transform itemPrefabParent;
    [SerializeField]
    private HorizontalLayoutGroup horizontalLayoutGroup; // HorizontalLayoutGroup���Q�Ƃ���t�B�[���h��ǉ�

    [SerializeField]
    private Text clickDebugText; // �e�X�g�p

    private ScrollRect scrollRect;
    private float imageWidth;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateCarouselItem(i, DebugEvent);
        }
        Init();


    }

    private void DebugEvent(int index)
    {
        clickDebugText.text = $"Clicked: {index}";
    }

    private void CreateCarouselItem(int index, Action<int> onClick)
    {
        CarouselItem item = Instantiate<CarouselItem>(itemPrefab, itemPrefabParent);
        item.SetImage(index, onClick);
    }

    void Init()
    {
        scrollRect = GetComponent<ScrollRect>();
        imageWidth = scrollRect.content.GetChild(0).GetComponent<RectTransform>().sizeDelta.x + horizontalLayoutGroup.spacing; // spacing���l������imageWidth���v�Z

        // �����ʒu�𒆉��̗v�f�ɐݒ�
        int totalImages = scrollRect.content.childCount;
        scrollRect.content.anchoredPosition = new Vector2(totalImages / 2 * imageWidth, 0);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float position = scrollRect.content.anchoredPosition.x;
        float targetPosition = Mathf.Round(position / imageWidth) * imageWidth;
        targetPosition += imageWidth / 2;  // �ł��߂��A�C�e���̒��S�Ɉړ�����

        scrollRect.content.anchoredPosition = new Vector2(targetPosition, 0);

    }
}