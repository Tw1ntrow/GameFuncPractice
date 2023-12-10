using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AccordionController : MonoBehaviour
{
    public RectTransform[] panels;
    public Image[] tabImages; // �^�C�g���摜
    public Sprite openTabSprite; // �^�u���J���Ă���Ƃ��̃^�C�g��
    public Sprite closedTabSprite; // �^�u�����Ă���Ƃ��̂����Ƃ�
    public float animationDuration = 0.5f; // �A�j���[�V��������
    public float openHeight = 200f; // �^�u���J���Ă���Ƃ��̍���

    public void TogglePanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            bool isOpen = panels[i].sizeDelta.y > 0;
            if (i == index && !isOpen)
            {
                // �p�l�����J���A�j���[�V����
                OpenPanel(panels[i]);
                tabImages[i].sprite = openTabSprite;
            }
            else
            {
                // �p�l�������A�j���[�V����
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