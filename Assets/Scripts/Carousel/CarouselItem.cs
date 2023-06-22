using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselItem : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private int index;
    [SerializeField]
    private Text text;// �e�X�g�p

    private Action<int> onClick;

    public void SetImage(int index, Action<int> onClick)
    {
        // ���炩�̕��@�ŉ摜��ݒ肷��z��
        //image.sprite = Resources.Load<Sprite>("Images/" + index);
        text.text = index.ToString();
        this.index = index;
        this.onClick = onClick;
    }

    public void OnClock()
    {
        onClick?.Invoke(index);
    }


}
