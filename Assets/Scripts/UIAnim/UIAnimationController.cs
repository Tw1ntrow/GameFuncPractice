using UnityEngine;
using DG.Tweening;

public class UIAnimationController : MonoBehaviour
{
    [SerializeField]
    private RectTransform[] uiElements; // �A�j���[�V����������UI
    [SerializeField]
    private float animationDuration = 1.0f; // �A�j���[�V�����̏��v����
    [SerializeField]
    private float maxDelay = 0.5f; // �ő�x������

    private Vector2[] targetPositions;

    void Awake()
    {
        // �eUI�v�f�̖ڕW�ʒu���L�^
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
            // �����ʒu����ʊO�ɐݒ�
            Vector2 startPosition = new Vector2(-Screen.width, uiElement.anchoredPosition.y);
            uiElement.anchoredPosition = startPosition;

            // �x�����Ԃ��v�Z
            float delay = maxDelay * (1 - (targetPositions[i].x + Screen.width) / Screen.width);

            // �ڕW�ʒu�ֈړ�
            uiElement.DOAnchorPos(targetPositions[i], animationDuration).SetEase(Ease.OutQuad).SetDelay(delay);
        }
    }
}