using UnityEngine;
using DG.Tweening;

public class UIAnimationController : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] uiElements; // アニメーションさせるUI
    [SerializeField]
    private float animationDuration = 1.0f; // アニメーションの所要時間
    [SerializeField]
    private float maxDelay = 0.5f; // 最大遅延時間

    private Vector2[] targetPositions;

    void Awake()
    {
        // 各UI要素の目標位置を記録
        targetPositions = new Vector2[uiElements.Length];
        for (int i = 0; i < uiElements.Length; i++)
        {
            targetPositions[i] = uiElements[i].anchoredPosition;
        }
    }

    void Start()
    {
        for (int i = 0; i < uiElements.Length; i++)
        {
            RectTransform uiElement = uiElements[i];
            // 初期位置を画面外に設定
            Vector2 startPosition = new Vector2(-Screen.width, uiElement.anchoredPosition.y);
            uiElement.anchoredPosition = startPosition;

            // 遅延時間を計算
            float delay = maxDelay * (1 - (targetPositions[i].x + Screen.width) / Screen.width);

            // 目標位置へ移動
            uiElement.DOAnchorPos(targetPositions[i], animationDuration).SetEase(Ease.OutQuad).SetDelay(delay);
        }
    }
}